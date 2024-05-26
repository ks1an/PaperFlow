using TMPro;
using UnityEngine;

public class Pause : FsmGameState
{
    private GameObject _pauseUI;
    private TextMeshProUGUI _scoreText;
    private Score _score;

    public Pause(FsmGame fsmUI, GameObject pauseUI, TextMeshProUGUI scoreText, Score score) : base(fsmUI)
    {
        _pauseUI = pauseUI;
        _scoreText = scoreText;
        _score = score;
    }

    public override void Enter()
    {
        base.Enter();

        Time.timeScale = 0f;

        _pauseUI.SetActive(true);
        FocusBackgroundPanel.FocusBackPanel.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

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
