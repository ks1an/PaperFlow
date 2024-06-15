using System;
using UnityEngine;

public class RandomWithSeed : MonoBehaviour
{
    int _curSeed;
    string _strCurSeed;
    string _lastSeedNumbers;
    int _x, _y, _z, _w, _t;

    public void GetFirstSeed()
    {
        _x = DateTime.Now.Second + 1;
        _y = DateTime.Now.Minute + 1;
        _z = DateTime.Now.Hour + 1;
        _w = DateTime.Now.Day;

        _curSeed = RefreshSeed();
        DebuginggManager.Log($"FirstSeed = {_curSeed}. x = {_x}, y = {_y}, z = {_z}");
    }

    public int RefreshSeed()
    {
        _t = _x ^ (_x << 11);
        _x = _y;
        _y = _z;
        _z = _w;
        _w = (_w ^ (_t >> 19)) ^ (_t ^ (_t >> 8));

        if(_w < 0)
            _w *= -1;

        if(_w < 10)
            _w *= 10;

        _curSeed = _w;

        return _curSeed;
    }

    public int Range(int min, int max)
    {
        RefreshSeed();
        return _curSeed % (max - min) + min + 1;
    }

    public bool CheckChance(int chance)
    {
        RefreshSeed();

        _strCurSeed = _curSeed.ToString();

        _lastSeedNumbers = "";
        for (int i = 1; i <= 2; i++)
            _lastSeedNumbers += _strCurSeed[^i];

        if(int.Parse(_lastSeedNumbers) <= chance) 
            return true;
        else 
            return false;
    }
}
