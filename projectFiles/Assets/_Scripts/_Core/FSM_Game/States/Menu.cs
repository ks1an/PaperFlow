using UnityEngine;

public sealed class Menu : FsmGameState
{
    GameStateController _controller;
    MenuUI _menuUI;
    GameObject _gameplayUI;

    public Menu(FsmGame fsmUI, GameStateController controller, MenuUI menuUI, GameObject gameplayUI) : base(fsmUI)
    {
        _controller = controller;
        _menuUI = menuUI;
        _gameplayUI = gameplayUI;
    }

    public override void Enter()
    {
        Time.timeScale = 0f;
        _gameplayUI.SetActive(false);
        _menuUI.SetActiveMenu(true);
        FocusBackgroundPanel.FocusBackPanel.SetActive(true);

        _controller.CallMenuEvent();
    }

    public override void Exit()
    {
        _menuUI.SetActiveMenu(false);
    }
}
