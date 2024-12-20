using System;
using UnityEngine;

public sealed class GameStateController : MonoBehaviour
{
    public static Action onMenuState;
    public static Action OnStartGameState;
    public static Action onPlayState;
    public static Action onPauseState;
    public static Action onGameOverState;

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
        _fsm = new FsmGame();

        _fsm.AddState(new Menu(_fsm, this, _menuUI, _gameplayUI));
        _fsm.AddState(new StartGame(_fsm, _gameplayUI, this));
        _fsm.AddState(new Play(_fsm, _pauseGameBttn, this));
        _fsm.AddState(new Pause(_fsm, _pauseUI, this));
        _fsm.AddState(new GameOver(_fsm, _gameOverUI, this));

        _fsm.SetState<Menu>();
    }

    #region CallEvent

    public void CallMenuEvent() => onMenuState?.Invoke();
    public void CallStartGameEvent() => OnStartGameState?.Invoke();
    public void CallPlayEvent() => onPlayState?.Invoke();
    public void CallPauseEvent() => onPauseState?.Invoke();
    public void CallGameOverEvent() => onGameOverState?.Invoke();

    #endregion

    #region SetStates

    public void SetMenuState() => _fsm.SetState<Menu>();
    public void SetStartGameState() => _fsm.SetState<StartGame>();
    public void SetPlayState() => _fsm.SetState<Play>();
    public void SetPauseState() => _fsm.SetState<Pause>();
    public void SetGameOverState() => _fsm.SetState<GameOver>();

    #endregion

    void Update() => _fsm.Update();
}
