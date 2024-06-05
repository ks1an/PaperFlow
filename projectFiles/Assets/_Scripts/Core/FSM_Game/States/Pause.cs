using TMPro;
using UnityEngine;

public class Pause : FsmGameState
{
    private GameObject _pauseUI;
    private TextMeshProUGUI _scoreText;
    private Score _score;
    private Player _player;

    public Pause(FsmGame fsmUI, GameObject pauseUI, TextMeshProUGUI scoreText, Score score, Player player) : base(fsmUI)
    {
        _pauseUI = pauseUI;
        _scoreText = scoreText;
        _score = score;
        _player = player;
    }

    public override void Enter()
    {
        base.Enter();

        Time.timeScale = 0f;

        _player.SetIdleState();
        _pauseUI.SetActive(true);
        FocusBackgroundPanel.FocusBackPanel.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        _player.SetMovementState();
        _pauseUI.SetActive(false);
    }

    public override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Escape))
            Fsm.SetState<Play>();

        _scoreText.text = $"{_score.CurrentScore}/{_score.RecordScore}";
    }
}
