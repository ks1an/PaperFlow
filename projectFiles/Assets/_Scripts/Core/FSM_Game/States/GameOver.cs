using UnityEngine;

public class GameOver : FsmGameState
{
    GameObject _gameOverUI;
    Score _score;
    Timer _timer;

    public GameOver(FsmGame fsmUI, GameObject gameOverUI, Score score, Timer timer) : base(fsmUI)
    {
        _gameOverUI = gameOverUI;
        _score = score;
        _timer = timer;
    }
    public override void Enter()
    {
        base.Enter();

        Time.timeScale = 0f;
        _timer.CompleteTimer();

        _gameOverUI.SetActive(true);
        FocusBackgroundPanel.FocusBackPanel.SetActive(true);

        if (_score.CurrentScore > _score.RecordScore)
        {
            DebuginggManager.DebugLog($"New record! Your new record: {_score.CurrentScore}. Your previous record: {_score.RecordScore}.");
            _score.SetNewRecord();
        }
    }

    public override void Exit()
    {
        base.Exit();

        _gameOverUI.SetActive(false);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.Space))
            Fsm.SetState<Menu>();
    }
}
