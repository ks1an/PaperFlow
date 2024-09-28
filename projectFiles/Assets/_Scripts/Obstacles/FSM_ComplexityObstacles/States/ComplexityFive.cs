public sealed class ComplexityFive : FsmComplexityControllerState
{
    ComplexityController _complexityController;

    public ComplexityFive(FsmComplexityController fsm, ComplexityController manager) : base(fsm)
    {
        _complexityController = manager;
    }

    public override void Enter()
    {
        base.Enter();

        _complexityController.SetSpeed(_complexityController.speedsComplexityes[4]);
    }
}
