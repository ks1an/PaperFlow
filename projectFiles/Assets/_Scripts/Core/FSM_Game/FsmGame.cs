using System;
using System.Collections.Generic;
using UnityEngine;

public class FsmGame
{
    private FsmGameState StateCurrent { get; set; }

    private Dictionary<Type, FsmGameState> _states = new Dictionary<Type, FsmGameState>();

    public void AddState(FsmGameState state) => _states.Add(state.GetType(), state);

    public void SetState<T>() where T : FsmGameState
    {
        var type = typeof(T);

        if(StateCurrent != null && StateCurrent.GetType() == type)
        {
            Debug.Log($"Trying to toggle {StateCurrent} state on FsmGame, but it's already on");
            return;
        }

        if(_states.TryGetValue(type, out var newState))
        {
            StateCurrent?.Exit();

            StateCurrent = newState;

            StateCurrent.Enter();

            Debug.Log($"The interface has entered the state {StateCurrent}");
        }
    }

    public void Update() => StateCurrent?.Update();
}
