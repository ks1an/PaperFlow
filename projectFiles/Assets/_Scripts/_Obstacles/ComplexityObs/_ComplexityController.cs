using System;
using UnityEngine;

public sealed class ComplexitySettingsInProcedure : MonoBehaviour
{
    public static float CurrentComplexitySpeed { get; private set; }

    public static Action OnComplexityAddedStamina;
    public static Action OnPurchasedBallSkillAndStaminaBar;
    public static Action OnNextComplexityTurned;

    [SerializeField] public float[] speedsComplexityes;
    [SerializeField] int[] _allObstaclesSpawnForLevel;

    FsmComplexityController _fsm;
    int _currentComplexityIndex;

    void Awake()
    {
        #region FSM/AddStates

        _fsm = new FsmComplexityController();

        _fsm.AddState(new Complexity_0(_fsm, this));
        _fsm.AddState(new Complexity_1(_fsm, this));
        _fsm.AddState(new Complexity_2(_fsm, this));
        _fsm.AddState(new Complexity_3(_fsm, this));
        _fsm.AddState(new Complexity_4(_fsm, this));
        _fsm.AddState(new Complexity_5(_fsm, this));
        _fsm.AddState(new Complexity_6(_fsm, this));
        _fsm.AddState(new Complexity_7(_fsm, this));
        _fsm.AddState(new Complexity_8(_fsm, this));
        _fsm.AddState(new Complexity_9(_fsm, this));
        _fsm.AddState(new Complexity_10(_fsm, this));
        _fsm.AddState(new Complexity_11(_fsm, this));
        _fsm.AddState(new Complexity_12(_fsm, this));
        #endregion
    }

    public void CheckNeddObtaclesForNextComplexity(int allObsAmount)
    {
        if (_currentComplexityIndex != _allObstaclesSpawnForLevel.Length-1 && allObsAmount >= _allObstaclesSpawnForLevel[_currentComplexityIndex + 1])
        {
            _currentComplexityIndex++;
            InvokeOnNextComplexityTurned();
        }
    }

    #region Set

    internal void SetSpeed(float value) => CurrentComplexitySpeed = value;

    internal void SetStartComplexity()
    {
        _currentComplexityIndex = 0;
        _fsm.SetState<Complexity_0>();
    }
    #endregion

    #region Invoke

    internal void InvokeOnComplexityAddedStamina() => OnComplexityAddedStamina?.Invoke();
    internal void InvokeOnObtainedBallSkillAndStaminaBar() => OnPurchasedBallSkillAndStaminaBar?.Invoke();
    internal void InvokeOnNextComplexityTurned() => OnNextComplexityTurned?.Invoke();
    #endregion

    private void OnEnable()
    {
        GameStateController.OnGameStarted += SetStartComplexity;
    }
    private void OnDisable()
    {
        GameStateController.OnGameStarted -= SetStartComplexity;
    }
}
