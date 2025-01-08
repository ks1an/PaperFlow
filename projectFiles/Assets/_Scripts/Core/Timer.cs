using System.Collections;
using UnityEngine;

public sealed class Timer : MonoBehaviour
{
    public string result;

    [SerializeField] bool stop;

    int _min, _sec;
    string _m, _s;

    WaitForSeconds _oneSecDelay = new(1);

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
    }

    void UpdateStringCurrentTIme()
    {
        if (_sec < 10) _s = "0" + _sec;
        else _s = _sec.ToString();

        if (_min < 10) _m = "0" + _min;
        else _m = _min.ToString();

        result = _m + ":" + _s;
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

        UpdateStringCurrentTIme();
    }

    void ResumeTimer() => stop = false;

    void PauseTimer() => stop = true;
    #endregion

    void CompleteTimer()
    {
        StopCoroutine(RepeatingFunction());
        UpdateStringCurrentTIme();
    }

    private void OnEnable()
    {
        GameStateController.OnStartProcedureGameState += StartTimer;
        GameStateController.onPlayState += ResumeTimer;
        GameStateController.onPauseState += PauseTimer;
        GameStateController.onGameOverState += CompleteTimer;
    }
    private void OnDisable()
    {
        GameStateController.OnStartProcedureGameState -= StartTimer;
        GameStateController.onPlayState -= ResumeTimer;
        GameStateController.onPauseState -= PauseTimer;
        GameStateController.onGameOverState -= CompleteTimer;
    }
}