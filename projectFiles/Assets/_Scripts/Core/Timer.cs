using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Action OnComplexityTimeTicked;

    public string result;

    [SerializeField] int _startMin, _startSec;
    [SerializeField] bool stop;

    int _min, _sec;
    string _m, _s;

    List<int> _complexityMin = new List<int>(), _complexitySec = new List<int>();
    int _currentComplexity = 1;

    void Awake()
    {
        if (_startMin > 0 && _startMin <= 59) _min = _startMin; else _startMin = 0;
        if (_startSec > 0 && _startSec <= 59) _sec = _startSec; else _startSec = 0;
    }

    public void SetComplexityTriggerTime(int complexityNumber, int min, int sec)
    {
        _complexityMin.Add(min);
        _complexitySec.Add(sec);
    }

    void ResetTimer()
    {
        _sec = 0;
        _min = 0;
        _currentComplexity = 1;

        CurrentTime();
    }

    IEnumerator RepeatingFunction()
    {
        while (true)
        {
            if (!stop) TimeCount();
            yield return new WaitForSeconds(1f);
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

        CurrentTime();
        CheckComplexityTriggerTime();
    }

    void CurrentTime()
    {
        if (_sec < 10) _s = "0" + _sec; else _s = _sec.ToString();
        if (_min < 10) _m = "0" + _min; else _m = _min.ToString();

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

    void StartTimer()
    {
        if (stop)
            stop = false;

        ResetTimer();
        StartCoroutine(RepeatingFunction());
    }

    void ResumeTimer() => stop = false;
    void PauseTimer() => stop = true;

    void CompleteTimer()
    {
        StopCoroutine(RepeatingFunction());
        CurrentTime();
    }

    private void OnEnable()
    {
        GameController.onStartGameState += StartTimer;
        GameController.onPlayState += ResumeTimer;
        GameController.onPauseState += PauseTimer;
        GameController.onGameOverState += CompleteTimer;
    }
    private void OnDisable()
    {
        GameController.onStartGameState -= StartTimer;
        GameController.onPlayState -= ResumeTimer;
        GameController.onPauseState -= PauseTimer;
        GameController.onGameOverState -= CompleteTimer;
    }
}