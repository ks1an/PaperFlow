using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("UIs")]
    [SerializeField] private GameObject _menuUI;
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private GameObject _gameplayUI;
    [SerializeField] private GameObject _gameOverUI;

    [Header("UI elements")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameObject _pauseGameBttn;

    [Header("Player")]
    [SerializeField] private Player _player;
    [SerializeField] private Health _playerHealthSystem;
    [SerializeField] private Stamina _playerStaminaSystem;
    [SerializeField] private Vector2 _startPos;

    [Header("Other")]
    [SerializeField] private Score _score;
    [SerializeField] private Timer _timer;
    [SerializeField] private ObstaclesManager _obstaclesManager;
    [SerializeField] private ObstaclesContainer _obstacleContainer;

    private FsmGame _fsm;

    private void Start()
    {
        _startPos = _player.transform.position;

        _fsm = new FsmGame();

        _fsm.AddState(new Menu(_fsm, _menuUI, _gameplayUI, _player, _startPos));
        _fsm.AddState(new StartGame(_fsm,  _gameplayUI, _score, _playerHealthSystem, _obstacleContainer, _playerStaminaSystem, _timer, _obstaclesManager));
        _fsm.AddState(new Play(_fsm, _gameplayUI, _scoreText, _score, _pauseGameBttn, _timer));
        _fsm.AddState(new Pause(_fsm, _pauseUI, _scoreText, _score, _player, _timer));
        _fsm.AddState(new GameOver(_fsm, _gameOverUI, _score, _timer));

        _fsm.SetState<Menu>();
    }

    private void Update() => _fsm.Update();

    public void SetMenuState() => _fsm.SetState<Menu>();
    public void SetPauseState() => _fsm.SetState<Pause>();
    public void SetPlayState() => _fsm.SetState<Play>();
    public void SetGameOverState() => _fsm.SetState<GameOver>();
}
