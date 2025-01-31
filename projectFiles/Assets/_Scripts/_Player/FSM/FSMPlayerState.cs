public abstract class FSMPlayerState
{
    protected readonly FsmPlayer Fsm;

    public FSMPlayerState(FsmPlayer fsm) => Fsm = fsm;

    public virtual void Enter() { }
    public virtual void FixedUpdate() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
