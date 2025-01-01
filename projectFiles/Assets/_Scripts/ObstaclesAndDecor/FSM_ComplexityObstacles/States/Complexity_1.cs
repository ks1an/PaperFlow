public sealed class Complexity_1 : FsmComplexityControllerState
{
    ComplexityController _complexityController;

    public Complexity_1(FsmComplexityController fsm, ComplexityController manager) : base(fsm)
    {
        _complexityController = manager;
    }

    public override void Enter()
    {
        base.Enter();

        Timer.OnComplexityTimeTicked += NextComplexity;

        _complexityController.SetSpeed(_complexityController.speedsComplexityes[1]);
    }

    public override void Exit()
    {
        base.Exit();

        Timer.OnComplexityTimeTicked -= NextComplexity;
    }

    void NextComplexity() => Fsm.SetState<Complexity_2>();
}
