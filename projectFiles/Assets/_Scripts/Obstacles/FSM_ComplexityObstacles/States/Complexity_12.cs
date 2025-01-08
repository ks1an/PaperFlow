public sealed class Complexity_12 : FsmComplexityControllerState
{
    ComplexitySettingsInProcedure _complexityController;

    public Complexity_12(FsmComplexityController fsm, ComplexitySettingsInProcedure manager) : base(fsm)
    {
        _complexityController = manager;
    }

    public override void Enter()
    {
        base.Enter();

        _complexityController.SetSpeed(_complexityController.speedsComplexityes[12]);
    }
}
