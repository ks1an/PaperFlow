using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class Seed : MonoBehaviour
{
    [SerializeField] string _firstSeed;
    [SerializeField] TMP_InputField _inputSeedTxt;

    RandomNumberGenerator _random = RandomNumberGenerator.GetInstance();

    void GetFirstSeed()
    {
        _firstSeed = "";

        if (_inputSeedTxt == null || !_inputSeedTxt.IsActive())
        {
            _firstSeed = DateTime.Now.Minute.ToString();
            while (_firstSeed.Length < 6)
                _firstSeed += Random.Range(0, 9).ToString();
        }
        else
        {
            _firstSeed = _inputSeedTxt.text;
        }

        _random.GetSeed(Convert.ToUInt32(_firstSeed));
    }

    private void OnEnable()
    {
        GameStateController.OnStartProcedureGameState += GetFirstSeed;
    }
    private void OnDisable()
    {
        GameStateController.OnStartProcedureGameState -= GetFirstSeed;
    }
}
