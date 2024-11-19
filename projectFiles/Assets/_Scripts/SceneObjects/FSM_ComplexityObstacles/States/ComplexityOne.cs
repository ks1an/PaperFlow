public sealed class ComplexityOne : FsmComplexityControllerState
{
    ComplexityController _complexityController;

    public ComplexityOne(FsmComplexityController fsm, ComplexityController manager) : base(fsm)
    {
        _complexityController = manager;
    }

    public override void Enter()
    {
        base.Enter();

        Timer.OnComplexityTimeTicked += NextComplexity;

        _complexityController.SetSpeed(_complexityController.speedsComplexityes[0]);
    }

    public override void Exit()
    {
        base.Exit();

        Timer.OnComplexityTimeTicked -= NextComplexity;
    }

    void NextComplexity() => Fsm.SetState<ComplexityTwo>();
}
