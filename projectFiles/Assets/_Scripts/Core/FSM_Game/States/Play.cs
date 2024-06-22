using UnityEngine;

public class Play : FsmGameState
{
    GameController _controller;
    GameObject _gameplayUI;
    GameObject _pauseBttn;

    public Play(FsmGame fsmUI, GameObject gameplayUI, GameObject pauseGameBttn,
        GameController controller) : base(fsmUI)
    {
        _controller = controller;
        _gameplayUI = gameplayUI;
        _pauseBttn = pauseGameBttn;
    }

    public override void Enter()
    {
        base.Enter();

        _controller.CallPlayEvent();
        _pauseBttn.SetActive(true);
        FocusBackgroundPanel.FocusBackPanel.SetActive(false);

        Time.timeScale = 1.0f;
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
    }
}
