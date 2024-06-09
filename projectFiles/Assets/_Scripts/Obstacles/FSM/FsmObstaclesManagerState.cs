public abstract class FsmObstaclesManagerState
{
    protected readonly FsmObstaclesManager Fsm;

    public FsmObstaclesManagerState(FsmObstaclesManager fsm) => Fsm = fsm;

    public virtual void Enter() { }
    public virtual void Exit() { }
}
