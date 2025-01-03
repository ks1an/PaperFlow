public sealed class Complexity_2 : FsmComplexityControllerState
{
    ComplexityController _complexityController;

    public Complexity_2(FsmComplexityController fsm, ComplexityController manager) : base(fsm)
    {
        _complexityController = manager;
    }

    public override void Enter()
    {
        base.Enter();

        Timer.OnComplexityTimeTicked += NextComplexity;

        _complexityController.SetSpeed(_complexityController.speedsComplexityes[2]);
        _complexityController.InvokeOnObtainedBallSkillAndStaminaBar();
    }

    public override void Exit()
    {
        base.Exit();

        Timer.OnComplexityTimeTicked -= NextComplexity;
    }

    void NextComplexity() => Fsm.SetState<Complexity_3>();
}
