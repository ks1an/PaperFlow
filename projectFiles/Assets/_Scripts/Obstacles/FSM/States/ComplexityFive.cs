public sealed class ComplexityFive : FsmObstaclesManagerState
{
    ComplexityController _complexityController;

    public ComplexityFive(FsmObstaclesManager fsm, ComplexityController manager) : base(fsm)
    {
        _complexityController = manager;
    }

    public override void Enter()
    {
        base.Enter();

        _complexityController.SetSpeed(_complexityController.speedsComplexityes[4]);
    }
}
