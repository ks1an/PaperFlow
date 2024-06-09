public class ComplexityFour : FsmObstaclesManagerState
{
    ObstaclesManager _manager;
    Timer _timer;
    public ComplexityFour(FsmObstaclesManager fsm, ObstaclesManager manager, Timer timer) : base(fsm)
    {
        _manager = manager;
        _timer = timer;
    }

    public override void Enter()
    {
        base.Enter();

        _timer.OnComplexityTimeTicked += NextComplexity;

        _manager.SetSpeed(_manager.speedFour);
    }

    public override void Exit()
    {
        base.Exit();

        _timer.OnComplexityTimeTicked -= NextComplexity;
    }

    void NextComplexity() => Fsm.SetState<ComplexityFive>();
}