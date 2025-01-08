using UnityEngine;

public sealed class ProcedureStartGame : FsmGameState
{
    GameStateController _controller;
    GameObject _gameplayUI;

    public ProcedureStartGame(FsmGame fsmUI, GameObject gameplayUI, GameStateController controller) : base(fsmUI)
    {
        _controller = controller;
        _gameplayUI = gameplayUI;
    }

    public override void Enter()
    {
        base.Enter();
        _controller.CallStartProcedureEvent();
        _gameplayUI.SetActive(true);


        Fsm.SetState<Play>();
    }
}