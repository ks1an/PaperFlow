public abstract class FsmDynamicObstacleState
{
    protected readonly FsmDynamicObstacle Fsm;

    public FsmDynamicObstacleState(FsmDynamicObstacle fsm) => Fsm = fsm;

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void LogicUpdate() { }
}
