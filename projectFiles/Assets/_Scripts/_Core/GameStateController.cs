using System;
using UnityEngine;

public sealed class GameStateController : MonoBehaviour
{
    #region OnAction

    public static Action OnMenuStarted;
    public static Action OnProcedureStarted;
    public static Action OnGameStarted;
    public static Action OnPlayStarted;
    public static Action OnPauseStarted;
    public static Action OnGameOverStarted;

    #endregion

    [SerializeField] Seed _seed;

    [Header("UIs")]
    [SerializeField] MenuUI _menuUI;
    [SerializeField] GameObject _pauseUI;
    [SerializeField] GameObject _gameplayUI;
    [SerializeField] GameObject _gameOverUI;

    [Header("Elements in UI")]
    [SerializeField] GameObject _pauseGameBttn;

    FsmGame _fsm;

    void Start()
    {
        #region FSM states
        _fsm = new FsmGame();

        _fsm.AddState(new Menu(_fsm, this, _menuUI, _gameplayUI));
        _fsm.AddState(new ProcedureGame(_fsm, this, _seed));
        _fsm.AddState(new GameStart(_fsm, _gameplayUI, this));
        _fsm.AddState(new Play(_fsm, _pauseGameBttn, this));
        _fsm.AddState(new Pause(_fsm, _pauseUI, this));
        _fsm.AddState(new GameOver(_fsm, _gameOverUI, this));
        #endregion

        _fsm.SetState<Menu>();
    }

    #region CallEvent

    public void CallMenuEvent() => OnMenuStarted?.Invoke();
    public void CallStartProcedureEvent() => OnProcedureStarted?.Invoke();
    public void CallGameStartEvent() => OnGameStarted?.Invoke();
    public void CallPlayEvent() => OnPlayStarted?.Invoke();
    public void CallPauseEvent() => OnPauseStarted?.Invoke();
    public void CallGameOverEvent() => OnGameOverStarted?.Invoke();

    #endregion

    #region SetStates

    public void SetMenuState() => _fsm.SetState<Menu>();
    public void SetStartProcedureGameState() => _fsm.SetState<ProcedureGame>();
    public void SetGameStart() => _fsm.SetState<GameStart>();
    public void SetPlayState() => _fsm.SetState<Play>();
    public void SetPauseState() => _fsm.SetState<Pause>();
    public void SetGameOverState() => _fsm.SetState<GameOver>();

    #endregion

    void Update() => _fsm.Update();
}
