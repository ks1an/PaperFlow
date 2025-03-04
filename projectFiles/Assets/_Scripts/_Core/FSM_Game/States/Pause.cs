using UnityEngine;

public sealed class Pause : FsmGameState
{
    GameStateController _controller;
    GameObject _pauseUI;

    public Pause(FsmGame fsmUI, GameObject pauseUI, GameStateController controller) : base(fsmUI)
    {
        _pauseUI = pauseUI;
        _controller = controller;
    }

    public override void Enter()
    {
        _controller.CallPauseEvent();
        Time.timeScale = 0f;

        _pauseUI.SetActive(true);
        FocusBackgroundPanel.FocusBackPanel.SetActive(true);
    }

    public override void Exit()
    {
        _pauseUI.SetActive(false);
    }
}
