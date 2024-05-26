using TMPro;
using UnityEngine;

public class Play : FsmGameState
{
    private GameObject _gameplayUI;
    private Score _score;
    private TextMeshProUGUI _scoreText;

    public Play(FsmGame fsmUI, GameObject gameplayUI, TextMeshProUGUI scoreText, Score score) : base(fsmUI)
    {
        _gameplayUI = gameplayUI;
        _scoreText = scoreText;
        _score = score;
    }

    public override void Enter()
    {
        base.Enter();

        FocusBackgroundPanel.FocusBackPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Escape))
            Fsm.SetState<Pause>();

        _scoreText.text = _score.CurrentScore.ToString();
    }
}
