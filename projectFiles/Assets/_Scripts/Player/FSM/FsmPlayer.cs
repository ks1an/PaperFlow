using System;
using System.Collections.Generic;

public class FsmPlayer
{
    public FSMPlayerState CurrentState { get; private set; }

    Dictionary<Type, FSMPlayerState> _states = new Dictionary<Type, FSMPlayerState>();

    public void AddState(FSMPlayerState state) => _states.Add(state.GetType(), state);

    public void SetState<T>() where T : FSMPlayerState
    {
        var type = typeof(T);

        if (CurrentState != null && CurrentState.GetType() == type)
        {
            DebuginggManager.Log($"Trying to toggle {CurrentState} state on FsmPlayer, but it's already on");
            return;
        }

        if(_states.TryGetValue(type, out var newState))
        {
            CurrentState?.Exit();

            CurrentState = newState;

            CurrentState.Enter();

            DebuginggManager.Log($"The player has entered the state {CurrentState}");
        }
    }

    public void Update() => CurrentState?.Update();
    public void FixedUpdate() => CurrentState?.FixedUpdate();
}
