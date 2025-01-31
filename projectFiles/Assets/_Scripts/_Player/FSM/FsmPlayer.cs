using System;
using System.Collections.Generic;
using UnityEngine;

public class FsmPlayer
{
    public FSMPlayerState CurrentState { get; private set; }

    Dictionary<Type, FSMPlayerState> _states = new();

    public void AddState(FSMPlayerState state) => _states.Add(state.GetType(), state);

    public void SetState<T>() where T : FSMPlayerState
    {
        var type = typeof(T);

        if (CurrentState != null && CurrentState.GetType() == type)
            return;

        if(_states.TryGetValue(type, out var newState))
        {
            CurrentState?.Exit();

            CurrentState = newState;

            CurrentState.Enter();
        }
    }

    public void Update() => CurrentState?.Update();
    public void FixedUpdate() => CurrentState?.FixedUpdate();
}
