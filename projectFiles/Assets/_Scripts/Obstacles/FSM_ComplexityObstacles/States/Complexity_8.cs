public sealed class Complexity_8 : FsmComplexityControllerState
{
    ComplexitySettingsInProcedure _complexityController;

    public Complexity_8(FsmComplexityController fsm, ComplexitySettingsInProcedure manager) : base(fsm)
    {
        _complexityController = manager;
    }

    public override void Enter()
    {
        base.Enter();

        ComplexitySettingsInProcedure.OnNextComplexityTurned += NextComplexity;

        _complexityController.SetSpeed(_complexityController.speedsComplexityes[8]);
    }

    public override void Exit()
    {
        base.Exit();

        ComplexitySettingsInProcedure.OnNextComplexityTurned -= NextComplexity;
    }

    void NextComplexity() => Fsm.SetState<Complexity_9>();
}
