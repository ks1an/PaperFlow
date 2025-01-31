using System;
using System.Collections.Generic;

public class FsmComplexityController
{
    public FsmComplexityControllerState CurrentState { get; private set; }

    public readonly Dictionary<Type, FsmComplexityControllerState> states = new();

    public void AddState(FsmComplexityControllerState state) => states.Add(state.GetType(), state);

    public void SetState<T>() where T : FsmComplexityControllerState
    {
        var type = typeof(T);

        if (CurrentState != null && CurrentState.GetType() == type)
            return;

        if (states.TryGetValue(type, out var newState))
        {
            CurrentState?.Exit();

            CurrentState = newState;

            CurrentState.Enter();
        }
    }
}
