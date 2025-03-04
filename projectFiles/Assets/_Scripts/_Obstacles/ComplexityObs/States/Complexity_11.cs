public sealed class Complexity_11 : FsmComplexityControllerState
{
    ComplexitySettingsInProcedure _complexityController;

    public Complexity_11(FsmComplexityController fsm, ComplexitySettingsInProcedure manager) : base(fsm)
    {
        _complexityController = manager;
    }

    public override void Enter()
    {
        ComplexitySettingsInProcedure.OnNextComplexityTurned += NextComplexity;
        _complexityController.SetSpeed(_complexityController.speedsComplexityes[11]);
    }

    public override void Exit()
    {
        ComplexitySettingsInProcedure.OnNextComplexityTurned -= NextComplexity;
    }

    void NextComplexity() => Fsm.SetState<Complexity_12>();
}
