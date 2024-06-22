using UnityEngine;

public class Menu : FsmGameState
{
    GameController _controller;
    GameObject _menuUI;
    GameObject _gameplayUI;

    public Menu(FsmGame fsmUI, GameController controller, GameObject menuUI, GameObject gameplayUI) : base(fsmUI)
    {
        _controller = controller;
        _menuUI = menuUI;
        _gameplayUI = gameplayUI;
    }

    public override void Enter()
    {
        base.Enter();

        _controller.CallMenuEvent();
        Time.timeScale = 0f;

        _menuUI.SetActive(true);
        _gameplayUI.SetActive(false);
        FocusBackgroundPanel.FocusBackPanel.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        _menuUI.SetActive(false);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
            Fsm.SetState<StartGame>();
    }
}
