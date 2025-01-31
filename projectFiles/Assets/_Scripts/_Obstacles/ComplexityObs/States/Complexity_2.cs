public sealed class Complexity_2 : FsmComplexityControllerState
{
    ComplexitySettingsInProcedure _complexityController;

    public Complexity_2(FsmComplexityController fsm, ComplexitySettingsInProcedure manager) : base(fsm)
    {
        _complexityController = manager;
    }

    public override void Enter()
    {
        ComplexitySettingsInProcedure.OnNextComplexityTurned += NextComplexity;

        _complexityController.SetSpeed(_complexityController.speedsComplexityes[2]);
        _complexityController.InvokeOnObtainedBallSkillAndStaminaBar();
    }

    public override void Exit()
    {
        ComplexitySettingsInProcedure.OnNextComplexityTurned -= NextComplexity;
    }

    void NextComplexity() => Fsm.SetState<Complexity_3>();
}
