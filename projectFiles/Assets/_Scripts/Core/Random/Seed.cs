using System;
using UnityEngine;

public sealed class Seed: MonoBehaviour
{
    [SerializeField] uint _firstSeed;
    [SerializeField] RandomNumberGenerator _numberGenerator;

    void GetFirstSeed()
    {
        _firstSeed = Convert.ToUInt32("11" + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString());

       _numberGenerator.GetSeed(_firstSeed);
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
