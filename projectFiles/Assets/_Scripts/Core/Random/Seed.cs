using System;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class Seed : MonoBehaviour
{
    [SerializeField] string _firstSeed;
    [SerializeField] bool _dontGenerateFirstSeed = false;
    [SerializeField] RandomNumberGenerator _numberGenerator;

    void GetFirstSeed()
    {
        if (!_dontGenerateFirstSeed)
        {
            _firstSeed = DateTime.Now.Second.ToString() + DateTime.Now.Minute.ToString();
            while (_firstSeed.Length < 4)
                _firstSeed += Random.Range(0, 9).ToString();
        }

        _numberGenerator.GetSeed(Convert.ToUInt32(_firstSeed));
    }

    private void OnEnable()
    {
        GameController.onStartGameState += GetFirstSeed;
    }
    private void OnDisable()
    {
        GameController.onStartGameState -= GetFirstSeed;
    }
}
