using TMPro;
using UnityEngine;

public class Play : FsmGameState
{
    GameObject _gameplayUI;
    Score _score;
    Timer _timer;
    TextMeshProUGUI _scoreText;
    GameObject _pauseBttn;

    public Play(FsmGame fsmUI, GameObject gameplayUI, TextMeshProUGUI scoreText, Score score, GameObject pauseGameBttn, Timer timer) : base(fsmUI)
    {
        _gameplayUI = gameplayUI;
        _scoreText = scoreText;
        _score = score;
        _pauseBttn = pauseGameBttn;
        _timer = timer;
    }

    public override void Enter()
    {
        base.Enter();

        _pauseBttn.SetActive(true);
        FocusBackgroundPanel.FocusBackPanel.SetActive(false);

        Time.timeScale = 1.0f;

        _timer.stop = false;
    }

    public override void Exit()
    {
        base.Exit();

        _pauseBttn.SetActive(false);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Escape))
            Fsm.SetState<Pause>();

        _scoreText.text = _score.CurrentScore.ToString();
    }
}
