using System;
using System.Collections.Generic;
using UnityEngine;

public class FsmComplexityController
{
    public FsmComplexityControllerState CurrentState { get; private set; }

    public readonly Dictionary<Type, FsmComplexityControllerState> states = new Dictionary<Type, FsmComplexityControllerState>();

    public void AddState(FsmComplexityControllerState state) => states.Add(state.GetType(), state);

    public void SetState<T>() where T : FsmComplexityControllerState
    {
        var type = typeof(T);

        if (CurrentState != null && CurrentState.GetType() == type)
        {
            Debug.Log($"Trying to toggle {CurrentState} state on FsmObstaclesManager, but it's already on");
            return;
        }

        if (states.TryGetValue(type, out var newState))
        {
            CurrentState?.Exit();

            CurrentState = newState;

            CurrentState.Enter();

            Debug.Log($"The ObstacleManager has entered the state {CurrentState}");
        }
    }
}
