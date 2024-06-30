public sealed class ComplexityOne : FsmObstaclesManagerState
{
    ObstacleManager _obstacleManager;
    Timer _timer;

    public ComplexityOne(FsmObstaclesManager fsm, ObstacleManager manager, Timer timer) : base(fsm)
    {
        _obstacleManager = manager;
        _timer = timer;
    }

    public override void Enter()
    {
        base.Enter();

        _timer.OnComplexityTimeTicked += NextComplexity;

        _obstacleManager.SetSpeed(_obstacleManager.speedsComplexityes[0]);
    }

    public override void Exit()
    {
        base.Exit();

        _timer.OnComplexityTimeTicked -= NextComplexity;
    }

    void NextComplexity() => Fsm.SetState<ComplexityTwo>();
}
