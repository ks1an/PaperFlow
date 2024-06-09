public class ComplexityFive : FsmObstaclesManagerState
{
    ObstaclesManager _manager;

    public ComplexityFive(FsmObstaclesManager fsm, ObstaclesManager manager) : base(fsm)
    {
        _manager = manager;
    }

    public override void Enter()
    {
        base.Enter();

        _manager.SetSpeed(_manager.speedFive);
    }
}
