// based: https://github.com/frozax/misc/blob/master/fgRandom/fgRandom.cs 28.06.24;

using System;
using UnityEngine;

public class RandomNumberGenerator : MonoBehaviour
{
    const int _n = 624;
    const int _m = 397;
    int _index = _n;
    readonly uint[] _mt = new uint[_n];

    string _firstNumbersStr;

    #region public methods
    public void GetSeed(uint seed)
    {
        _mt[0] = seed;

        for (uint i = 1; i < _n; i++)
            _mt[i] = 1812433253 * (_mt[i - 1] ^ (_mt[i - 1] >> 30)) + i;
    }

    public uint NextUInt() { return ExtractNumber(); }

    /// <summary>
    /// between min (included) and max (excluded)
    /// </summary>
    public int Range(int min, int max) { return (int)(NextUInt() % ((max) - min) + min);}

    public bool CheckChance(int chance)
    {
        _firstNumbersStr = NextUInt().ToString()[..2];

        if (Convert.ToInt16(_firstNumbersStr) <= chance)
            return true;
        else
            return false;
    }
    #endregion

    uint ExtractNumber()
    {
        if (_index >= _n)
            Twist();

        uint y = _mt[_index];

        // right shift by 11 bits
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
