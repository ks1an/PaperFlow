using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static float CurrentSpeedObstacles { get; private set; } = 5f;

    #region Complexity Settings
    public int[] _minForComplexityNum, _secForComplexityNum;
    [Space(10)]
    public float[] _speedsComplexityes = { 5.5f, 6f, 6.5f, 7.25f, 8f};
    [Space(10)]
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

    private void OnEnable()
    {
        GameController.onStartGameState += SetStartComplexity;
    }
    private void OnDisable()
    {
        GameController.onStartGameState -= SetStartComplexity;
    }
}
