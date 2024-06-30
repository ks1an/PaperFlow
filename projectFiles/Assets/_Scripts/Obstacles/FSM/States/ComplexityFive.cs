public sealed class ComplexityFive : FsmObstaclesManagerState
{
    ObstacleManager _manager;

    public ComplexityFive(FsmObstaclesManager fsm, ObstacleManager manager) : base(fsm)
    {
        _manager = manager;
    }

    public override void Enter()
    {
        base.Enter();

        _manager.SetSpeed(_manager.speedsComplexityes[4]);
    }
}
