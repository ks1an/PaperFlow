using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static float CurrentSpeedObstacles { get; private set; } = 5f;

    #region Complexity Settings
    public int[] _minForComplexityNum, _secForComplexityNum;
    [Space(5)]
    public float speedOne = 5f;
    [Space(10)]
    public float speedTwo = 5.25f;
    [Space(10)]
    public float speedThree = 5.5f;
    [Space(10)]
    public float speedFour = 6f;
    [Space(10)]
    public float speedFive = 8f;
    #endregion

    [SerializeField] Timer _timer;
    [SerializeField] Spawner _spawner;

    FsmObstaclesManager _fsm;

    private void Awake()
    {
        _fsm = new FsmObstaclesManager();

        _fsm.AddState(new ComplexityOne(_fsm, this, _timer));
        _fsm.AddState(new ComplexityTwo(_fsm, this, _timer));
        _fsm.AddState(new ComplexityThree(_fsm, this, _timer));
        _fsm.AddState(new ComplexityFour(_fsm, this, _timer));
        _fsm.AddState(new ComplexityFive(_fsm, this));

        for (int i = 0; i < _fsm.states.Count; i++)
            _timer.SetComplexityTriggerTime(i, _minForComplexityNum[i], _secForComplexityNum[i]);

        _fsm.SetState<ComplexityOne>();
    }

    public void SetSpeed(float value) => CurrentSpeedObstacles = value;

    public void SetStartComplexity() => _fsm.SetState<ComplexityOne>();

    #region Spawning Obstacles
    public void StartSpawning() => _spawner.StartSpawning();

    public void PauseSpawning() => _spawner.PauseSpawning();

    public void ResumSpawning() => _spawner.ResumeSpawning();

    public void StopPawning() => _spawner.StopSpawning();
    #endregion
}
