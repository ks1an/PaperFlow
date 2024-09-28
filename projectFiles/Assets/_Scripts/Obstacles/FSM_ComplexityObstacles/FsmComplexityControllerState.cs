public abstract class FsmComplexityControllerState
{
    protected readonly FsmComplexityController Fsm;

    public FsmComplexityControllerState(FsmComplexityController fsm) => Fsm = fsm;

    public virtual void Enter() { }
    public virtual void Exit() { }
}
