using System.Collections.Generic;
using System;

public class FsmDynamicObstacle
{
    public FsmDynamicObstacleState CurrentState { get; private set; }

    public readonly Dictionary<Type, FsmDynamicObstacleState> states = new Dictionary<Type, FsmDynamicObstacleState>();

    public void AddState(FsmDynamicObstacleState state) => states.Add(state.GetType(), state);

    public void SetState<T>() where T : FsmDynamicObstacleState
    {
        var type = typeof(T);

        if (CurrentState != null && CurrentState.GetType() == type)
        {
            DebuginggManager.Log($"Trying to toggle {CurrentState} state on FsmDynamicObstacles, but it's already on");
            return;
        }

        if (states.TryGetValue(type, out var newState))
        {
            CurrentState?.Exit();

            CurrentState = newState;

            CurrentState.Enter();

            DebuginggManager.Log($"The FsmDynamicObstacles has entered the state {CurrentState}");
        }
    }

    public void Update() => CurrentState?.Update();
    public void LogicUpdate() => CurrentState?.LogicUpdate();
}
