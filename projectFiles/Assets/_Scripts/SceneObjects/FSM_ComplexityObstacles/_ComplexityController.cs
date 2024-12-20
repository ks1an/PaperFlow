using System;
using UnityEngine;

public sealed class ComplexityController : MonoBehaviour
{
    #region Public statics
    public static Action OnPurchasedBallSkillAndStaminaBar;
    public static Action OnComplexityAddedStamina;

    public static float CurrentComplexitySpeed { get; private set; }
    #endregion

    [Space(10), Header("Settings")]
    public float[] speedsComplexityes;
    [SerializeField, Tooltip("format: min:sec|")] string[] _timeForComplexityNum;

    [Space(10)]
    [SerializeField] Timer _timer;

    FsmComplexityController _fsm;
    int[] _minForComplexityNum, _secForComplexityNum;

    private void Awake()
    {
        GameStateController.OnStartGameState += SetStartComplexity;

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

        #endregion

        #region SetTimeSetting

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
            _timer.SetComplexityTriggerTime(_minForComplexityNum[i], _secForComplexityNum[i]);
        #endregion

        _fsm.SetState<Complexity_0>();
    }

    #region Set

    internal void SetSpeed(float value) => CurrentComplexitySpeed = value;

    internal void SetStartComplexity() => _fsm.SetState<Complexity_0>();
    #endregion

    internal void InvokeOnComplexityAddedStamina() => OnComplexityAddedStamina?.Invoke();
    internal void InvokeOnObtainedBallSkillAndStaminaBar()
    {
        OnPurchasedBallSkillAndStaminaBar?.Invoke();
        Debug.Log("Invoke");
    }
}
