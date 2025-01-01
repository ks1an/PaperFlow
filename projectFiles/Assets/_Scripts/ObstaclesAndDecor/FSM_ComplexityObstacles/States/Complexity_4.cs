public sealed class Complexity_4 : FsmComplexityControllerState
{
    ComplexityController _complexityController;

    public Complexity_4(FsmComplexityController fsm, ComplexityController manager) : base(fsm)
    {
        _complexityController = manager;
    }

    public override void Enter()
    {
        base.Enter();

        Timer.OnComplexityTimeTicked += NextComplexity;

        _complexityController.SetSpeed(_complexityController.speedsComplexityes[4]);
        _complexityController.InvokeOnComplexityAddedStamina();
    }

    public override void Exit()
    {
        base.Exit();

        Timer.OnComplexityTimeTicked -= NextComplexity;
    }

    void NextComplexity() => Fsm.SetState<Complexity_5>();
}
