// based: https://github.com/frozax/misc/blob/master/fgRandom/fgRandom.cs 28.06.24;

using System;

public class RandomNumberGenerator
{
    #region singleton
    private RandomNumberGenerator() { }

    private static RandomNumberGenerator _instance;
    public static RandomNumberGenerator GetInstance()
    {
        _instance ??= new RandomNumberGenerator();
        return _instance;
    }
    #endregion

    const int _n = 624;
    const int _m = 397;
    int _index = _n;
    uint[] _mt = new uint[_n];

    string _firstNumbersStr;

    #region public methods
    public void GetSeed(uint seed)
    {
        _instance._mt[0] = seed;
        _instance._index = _n;

        for (uint i = 1; i < _n; i++)
            _instance._mt[i] = 1812433253 * (_instance._mt[i - 1] ^ (_instance._mt[i - 1] >> 30)) + i;
    }

    public uint NextUInt() { return _instance.ExtractNumber(); }

    /// <summary>
    /// between min (included) and max (excluded)
    /// </summary>
    public int Range(int min, int max) { return (int)(NextUInt() % ((max) - min) + min); }

    public float RangeFloat(float min, float max) { return ((NextFloat() * (max - min)) + min); }

    public float NextFloat() { return (float)(NextUInt() % 65536) / 65535.0f; }

    public bool CheckChance(int chance)
    {
        _instance._firstNumbersStr = NextUInt().ToString()[..2];

        return Convert.ToInt16(_instance._firstNumbersStr) <= chance;
    }
    #endregion

    uint ExtractNumber()
    {
        if (_index >= _n)
            Twist();

        uint y = _mt[_index];

        y = y ^ (y >> 11);
        y = y ^ ((y << 7) & 2636928640);
        y = y ^ ((y << 15) & 4022730752);
        y = y ^ (y >> 18);
        _index++;

        return y;
    }

    void Twist()
    {
        for (int i = 0; i < _n; i++)
        {
            uint y = ((_mt[i]) & 0x80000000) +
                ((_mt[(i + 1) % _n]) & 0x7fffffff);
            _mt[i] = _mt[(i + _m) % _n] ^ (uint)(y >> 1);
            if (y % 2 != 0)
                _mt[i] = _mt[i] ^ 0x9908b0df;
        }

        _index = 0;
    }
}
