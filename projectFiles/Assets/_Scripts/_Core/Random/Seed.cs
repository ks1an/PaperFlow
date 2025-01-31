using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class Seed : MonoBehaviour
{
    [SerializeField] TMP_InputField _inputSeedTxt;

    string _firstSeed;
    readonly RandomNumberGenerator _random = RandomNumberGenerator.GetInstance();

    public void GetFirstSeed()
    {
        _firstSeed = "";
        if (!_inputSeedTxt.gameObject.activeSelf || _inputSeedTxt.text.Length == 0 || _inputSeedTxt.text[0] == '-')
        {
            _firstSeed = DateTime.Now.Minute.ToString();
            while (_firstSeed.Length < 6)
                _firstSeed += Random.Range(1, 9).ToString();
        }
        else
            _firstSeed = _inputSeedTxt.text;

        _random.SetSeed(Convert.ToUInt32(_firstSeed));
    }
}
