using UnityEngine;

public sealed class Pause : FsmGameState
{
    GameController _controller;
    GameObject _pauseUI;

    public Pause(FsmGame fsmUI, GameObject pauseUI, GameController controller) : base(fsmUI)
    {
        _pauseUI = pauseUI;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();

        _controller.CallPauseEvent();
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
    }
}
