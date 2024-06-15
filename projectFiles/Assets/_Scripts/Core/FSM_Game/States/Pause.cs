using TMPro;
using UnityEngine;

public class Pause : FsmGameState
{
    GameObject _pauseUI;
    TextMeshProUGUI _scoreText;
    Score _score;
    Timer _timer;
    Player _player;
    ObstacleManager _obstaclesManager;

    public Pause(FsmGame fsmUI, GameObject pauseUI, TextMeshProUGUI scoreText, Score score, Player player, 
        Timer timer, ObstacleManager obstaclesManager) : base(fsmUI)
    {
        _pauseUI = pauseUI;
        _scoreText = scoreText;
        _score = score;
        _player = player;
        _timer = timer;
        _obstaclesManager = obstaclesManager;
    }

    public override void Enter()
    {
        base.Enter();

        Time.timeScale = 0f;

        _timer.stop = true;
        _obstaclesManager.PauseSpawning();

        _player.SetIdleState();
        _pauseUI.SetActive(true);
        FocusBackgroundPanel.FocusBackPanel.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        _player.SetMovementState();
        _pauseUI.SetActive(false);

        _obstaclesManager.ResumSpawning();
    }

    public override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Escape))
            Fsm.SetState<Play>();

        _scoreText.text = $"{_score.CurrentScore}/{_score.RecordScore}";
    }
}
