public sealed class ComplexityThree : FsmObstaclesManagerState
{
    ComplexityController _complextiyController;

    public ComplexityThree(FsmObstaclesManager fsm, ComplexityController manager) : base(fsm)
    {
        _complextiyController = manager; 
    }

    public override void Enter()
    {
        base.Enter();

        Timer.OnComplexityTimeTicked += NextComplexity;

        _complextiyController.SetSpeed(_complextiyController.speedsComplexityes[2]);
    }

    public override void Exit()
    {
        base.Exit();

        Timer.OnComplexityTimeTicked -= NextComplexity;
    }

    void NextComplexity() => Fsm.SetState<ComplexityFour>();
}
