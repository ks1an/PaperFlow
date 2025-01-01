public sealed class Complexity_9 : FsmComplexityControllerState
{
    ComplexityController _complexityController;

    public Complexity_9(FsmComplexityController fsm, ComplexityController manager) : base(fsm)
    {
        _complexityController = manager;
    }

    public override void Enter()
    {
        base.Enter();

        _complexityController.SetSpeed(_complexityController.speedsComplexityes[9]);
    }
}
