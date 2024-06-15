using System;
using System.Collections.Generic;

public class FsmObstaclesManager
{
    public FsmObstaclesManagerState CurrentState { get; private set; }

    public readonly Dictionary<Type, FsmObstaclesManagerState> states = new Dictionary<Type, FsmObstaclesManagerState>();

    public void AddState(FsmObstaclesManagerState state) => states.Add(state.GetType(), state);

    public void SetState<T>() where T : FsmObstaclesManagerState
    {
        var type = typeof(T);

        if (CurrentState != null && CurrentState.GetType() == type)
        {
            DebuginggManager.Log($"Trying to toggle {CurrentState} state on FsmObstaclesManager, but it's already on");
            return;
        }

        if (states.TryGetValue(type, out var newState))
        {
            CurrentState?.Exit();

            CurrentState = newState;

            CurrentState.Enter();

            DebuginggManager.Log($"The ObstacleManager has entered the state {CurrentState}");
        }
    }
}
