using UnityEngine;

public class GameOver : FsmGameState
{
    GameController _controller;
    GameObject _gameOverUI;

    public GameOver(FsmGame fsmUI, GameObject gameOverUI, GameController controller) : base(fsmUI)
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
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.Space))
            Fsm.SetState<Menu>();
    }
}
