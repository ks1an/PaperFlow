public sealed class ProcedureGame : FsmGameState
{
    GameStateController _controller;
    Seed _seed;

    public ProcedureGame(FsmGame fsmUI, GameStateController controller, Seed seed) : base(fsmUI)
    {
        _controller = controller;
        _seed = seed;
    }

    public override void Enter()
    {
        _seed.GetFirstSeed();
        _controller.CallStartProcedureEvent();

        Fsm.SetState<GameStart>();
    }
}