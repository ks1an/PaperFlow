public sealed class Complexity_10 : FsmComplexityControllerState
{
    ComplexitySettingsInProcedure _complexityController;

    public Complexity_10(FsmComplexityController fsm, ComplexitySettingsInProcedure manager) : base(fsm)
    {
        _complexityController = manager;
    }

    public override void Enter()
    {
        ComplexitySettingsInProcedure.OnNextComplexityTurned += NextComplexity;
        _complexityController.SetSpeed(_complexityController.speedsComplexityes[10]);
    }

    public override void Exit()
    {
        ComplexitySettingsInProcedure.OnNextComplexityTurned -= NextComplexity;
    }

    void NextComplexity() => Fsm.SetState<Complexity_11>();
}
