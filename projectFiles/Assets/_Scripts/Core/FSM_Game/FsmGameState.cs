public abstract class FsmGameState
{
    protected readonly FsmGame Fsm;

    public FsmGameState(FsmGame fsmUI)
    {
        Fsm = fsmUI;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}


