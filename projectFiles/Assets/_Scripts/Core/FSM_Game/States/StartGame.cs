using UnityEngine;

public sealed class StartGame : FsmGameState
{
    GameStateController _controller;
    GameObject _gameplayUI;

    public StartGame(FsmGame fsmUI, GameObject gameplayUI, GameStateController controller) : base(fsmUI)
    {
        _controller = controller;
        _gameplayUI = gameplayUI;
    }

    public override void Enter()
    {
        base.Enter();
        _controller.CallStartGameEvent();
        _gameplayUI.SetActive(true);


        Fsm.SetState<Play>();
    }
}