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
        Time.timeScale = 0f;
        _gameOverUI.SetActive(true);
        FocusBackgroundPanel.FocusBackPanel.SetActive(true);

        _controller.CallGameOverEvent();
    }

    public override void Exit()
    {
        _gameOverUI.SetActive(false);
        System.GC.Collect();
    }
}
