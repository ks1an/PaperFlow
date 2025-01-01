using UnityEngine;

public sealed class GameOver : FsmGameState
{
    GameStateController _controller;
    GameObject _gameOverUI;

    public GameOver(FsmGame fsmUI, GameObject gameOverUI, GameStateController controller) : base(fsmUI)
    {
        _controller = controller;
        _gameOverUI = gameOverUI;
    }
    public override void Enter()
    {
        base.Enter();

        _controller.CallGameOverEvent();
        Time.timeScale = 0f;

        _gameOverUI.SetActive(true);
        FocusBackgroundPanel.FocusBackPanel.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        _gameOverUI.SetActive(false);
        System.GC.Collect();
    }
}
