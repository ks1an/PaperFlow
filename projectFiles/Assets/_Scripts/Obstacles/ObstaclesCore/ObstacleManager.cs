using UnityEngine;

public sealed class ObstacleManager : MonoBehaviour
{
    public static float CurrentSpeedObstacles { get; private set; } = 5f;

    #region Complexity Settings
    [Space(10)]
    public float[] speedsComplexityes = { 5.5f, 6f, 6.5f, 7.25f, 8f };

    [SerializeField] string[] _timeForComplexityNum;
    [Space(10)]
    #endregion

    [SerializeField] Timer _timer;
    [SerializeField] Spawner _spawner;

    FsmObstaclesManager _fsm;
    int[] _minForComplexityNum, _secForComplexityNum;

    private void Awake()
    {
        _fsm = new FsmObstaclesManager();

        _fsm.AddState(new ComplexityOne(_fsm, this, _timer));
        _fsm.AddState(new ComplexityTwo(_fsm, this, _timer));
        _fsm.AddState(new ComplexityThree(_fsm, this, _timer));
        _fsm.AddState(new ComplexityFour(_fsm, this, _timer));
        _fsm.AddState(new ComplexityFive(_fsm, this));

        _minForComplexityNum = new int[_timeForComplexityNum.Length];
        _secForComplexityNum = new int[_timeForComplexityNum.Length];

        int j = 0;
        foreach (string s in _timeForComplexityNum)
        {
            string timeStr = "";
            int chapter = 0;
            foreach (char c in s)
            {

                if (c == ':' || c == '|')
                {
                    int t = int.Parse(timeStr.ToString());

                    if (chapter == 0)
                        _minForComplexityNum[j] = t;
                    else if (chapter == 1)
                        _secForComplexityNum[j] = t;

                    timeStr = " ";
                    chapter++;
                }
                else if (chapter > 1 || c == ' ')
                    break;
                else
                    timeStr += c;
            }
            j++;
        }

        for (int i = 0; i < _fsm.states.Count; i++)
            _timer.SetComplexityTriggerTime(i, _minForComplexityNum[i], _secForComplexityNum[i]);

        _fsm.SetState<ComplexityOne>();
    }

    public void SetSpeed(float value) => CurrentSpeedObstacles = value;

    public void SetStartComplexity() => _fsm.SetState<ComplexityOne>();

    private void OnEnable()
    {
        GameStateController.onStartGameState += SetStartComplexity;
    }
    private void OnDisable()
    {
        GameStateController.onStartGameState -= SetStartComplexity;
    }
}
