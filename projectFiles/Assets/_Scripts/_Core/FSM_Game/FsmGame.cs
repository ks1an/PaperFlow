using System;
using System.Collections.Generic;

public class FsmGame
{
    private FsmGameState StateCurrent { get; set; }

    private Dictionary<Type, FsmGameState> _states = new();

    public void AddState(FsmGameState state) => _states.Add(state.GetType(), state);

    public void SetState<T>() where T : FsmGameState
    {
        var type = typeof(T);

        if(StateCurrent != null && StateCurrent.GetType() == type)
            return;

        if(_states.TryGetValue(type, out var newState))
        {
            StateCurrent?.Exit();

            StateCurrent = newState;

            StateCurrent.Enter();
        }
    }

    public void Update() => StateCurrent?.Update();
}
