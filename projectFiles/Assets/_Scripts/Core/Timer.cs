using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Timer : MonoBehaviour
{
    public static event Action OnComplexityTimeTicked;

    public string result;

    [SerializeField] bool stop;

    int _min, _sec;
    string _m, _s;

    List<int> _complexityMin = new(), _complexitySec = new();
    int _currentComplexity = 0;

    WaitForSeconds _oneSecDelay = new(1);

    internal void SetComplexityTriggerTime(int min, int sec)
    {
        _complexityMin.Add(min);
        _complexitySec.Add(sec);
    }

    IEnumerator RepeatingFunction()
    {
        while (true)
        {
            if (!stop) 
                TimeCount();
            yield return _oneSecDelay;
        }
    }

    void TimeCount()
    {
        _sec++;

        if (_sec > 59)
        {
            _sec = 0;
            _min++;
        }

        CheckComplexityTriggerTime();
    }

    void UpdateCurrentTime()
    {
        if (_sec < 10) _s = "0" + _sec; 
        else _s = _sec.ToString();

        if (_min < 10) _m = "0" + _min;
        else _m = _min.ToString();

        result = _m + ":" + _s;
    }

    void CheckComplexityTriggerTime()
    {
        if (_currentComplexity < _complexityMin.Count && _sec == _complexitySec[_currentComplexity] && _min == _complexityMin[_currentComplexity])
        {
            _currentComplexity++;
            OnComplexityTimeTicked?.Invoke();
        }
    }

    #region TimerFunc

    void StartTimer()
    {
        if (stop)
            stop = false;

        ResetTimer();
        StartCoroutine(RepeatingFunction());
    }

    void ResetTimer()
    {
        _sec = 0;
        _min = 0;
        _currentComplexity = 1;

        UpdateCurrentTime();
    }

    void ResumeTimer() => stop = false;

    void PauseTimer() => stop = true;
    #endregion

    void CompleteTimer()
    {
        StopCoroutine(RepeatingFunction());
        UpdateCurrentTime();
    }

    private void OnEnable()
    {
        GameStateController.OnStartGameState += StartTimer;
        GameStateController.onPlayState += ResumeTimer;
        GameStateController.onPauseState += PauseTimer;
        GameStateController.onGameOverState += CompleteTimer;
    }
    private void OnDisable()
    {
        GameStateController.OnStartGameState -= StartTimer;
        GameStateController.onPlayState -= ResumeTimer;
        GameStateController.onPauseState -= PauseTimer;
        GameStateController.onGameOverState -= CompleteTimer;
    }
}