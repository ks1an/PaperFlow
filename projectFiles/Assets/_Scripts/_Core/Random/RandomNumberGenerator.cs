// based: https://github.com/frozax/misc/blob/master/fgRandom/fgRandom.cs 28.06.24;

public sealed class RandomNumberGenerator
{
    #region Singleton

    RandomNumberGenerator() { }
    static RandomNumberGenerator _instance;
    static readonly object _lock = new();

    public static RandomNumberGenerator GetInstance()
    {
        if (_instance == null)
            lock (_lock)
            {
                _instance ??= new RandomNumberGenerator();
            }

        return _instance;
    }

    #endregion

    const int _n = 512;
    const int _m = 397;

    int _index;
    uint[] _mt = new uint[_n];
    uint mySeed;

    public void SetSeed(uint seed)
    {
        seed = mySeed;
        _mt[0] = mySeed;
        for (uint i = 1; i < _n; i++)
        {
            _mt[i] = 1812433253 * (_mt[i - 1] ^ (_mt[i - 1] >> 30)) + i;
        }
        Twist();
    }

    #region Useful public methods

    /// <summary>
    /// between min (included) and max (excluded)
    /// </summary>
    public int Range(int min, int max) { return (int)(NextUInt() % (max - min) + min); }

    public float RangeFloat(float min, float max) { return ((NextFloat() * (max - min)) + min); }

    public uint NextUInt() { return ExtractNumber(); }

    public float NextFloat() { return (float)(NextUInt() % 65536) / 65535.0f; }

    public bool CheckChance(int chance)
    {
        uint firstDigits = NextUInt();

        while (firstDigits >= 100)
            firstDigits /= 10;

        return firstDigits <= chance;
    }
    #endregion

    #region Private methods

    uint ExtractNumber()
    {
        if (_index >= _n)
            Twist();

        uint y = _mt[_index];

        y ^= (y >> 11);
        y ^= ((y << 7) & 2636928640);
        y ^= ((y << 15) & 4022730752);
        y ^= (y >> 18);
        _index++;

        return y;
    }

    void Twist()
    {
        _mt[0] = mySeed;
        for (int i = 1; i < _n; i++)
        {
            uint y = ((_mt[i]) & 0x80000000) +
                ((_mt[(i + 1) % _n]) & 0x7fffffff);

            _mt[i] = _mt[(i + _m) % _n] ^ (uint)(y >> 1);
            if (y % 2 != 0)
                _mt[i] = _mt[i] ^ 0x9908b0df;
        }

        _index = 0;
    }

    #endregion
}
