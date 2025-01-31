using UnityEngine;

public sealed class GameStart : FsmGameState
{
    GameStateController _controller;
    GameObject _gameplayUI;

    public GameStart(FsmGame fsmUI, GameObject gameplayUI, GameStateController controller) : base(fsmUI)
    {
        _gameplayUI = gameplayUI;
        _controller = controller;
    }

    public override void Enter()
    {
        _gameplayUI.SetActive(true);

        _controller.CallGameStartEvent();

        Fsm.SetState<Play>();
    }
}
