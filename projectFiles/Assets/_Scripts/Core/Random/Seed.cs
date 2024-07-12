using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class Seed : MonoBehaviour
{
    [SerializeField] string _firstSeed;
    [SerializeField] RandomNumberGenerator _numberGenerator;
    [SerializeField] TMP_InputField _inputSeedTxt;

    void GetFirstSeed()
    {
        if (_inputSeedTxt.gameObject.activeSelf && _inputSeedTxt.text.Length > 2 && _inputSeedTxt.text[0] != '0')
            _firstSeed = _inputSeedTxt.text;
        else
        {
            _firstSeed = DateTime.Now.Second.ToString() + DateTime.Now.Minute.ToString();
            while (_firstSeed.Length < 4)
                _firstSeed += Random.Range(0, 9).ToString();
        }

        _numberGenerator.GetSeed(Convert.ToUInt32(_firstSeed));
    }

    private void OnEnable()
    {
        GameStateController.onStartGameState += GetFirstSeed;
    }
    private void OnDisable()
    {
        GameStateController.onStartGameState -= GetFirstSeed;
    }
}
