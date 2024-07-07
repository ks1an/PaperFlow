using Assets._Scripts.UI.Menu;
using UnityEngine;

public sealed class Menu : FsmGameState
{
    GameController _controller;
    MenuUI _menuUI;
    GameObject _gameplayUI;

    public Menu(FsmGame fsmUI, GameController controller, MenuUI menuUI, GameObject gameplayUI) : base(fsmUI)
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

        _gameplayUI.SetActive(false);
        _menuUI.MenuIntro();
        FocusBackgroundPanel.FocusBackPanel.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        _menuUI.MenuOutro();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
            Fsm.SetState<StartGame>();
    }
}
