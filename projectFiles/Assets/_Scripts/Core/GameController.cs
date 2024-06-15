using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("UIs")]
    [SerializeField] GameObject _menuUI;
    [SerializeField] GameObject _pauseUI;
    [SerializeField] GameObject _gameplayUI;
    [SerializeField] GameObject _gameOverUI;

    [Header("UI elements")]
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] GameObject _pauseGameBttn;

    [Header("Player")]
    [SerializeField] Player _player;
    [SerializeField] Health _playerHealthSystem;
    [SerializeField] Stamina _playerStaminaSystem;
    [SerializeField] Vector2 _startPos;

    [Header("Other")]
    [SerializeField] ObstacleManager _obstacleManager;
    [SerializeField] ObstaclesContainer _obstacleContainer;

    private FsmGame _fsm;
    Score _score; 
    Timer _timer;
    RandomWithSeed _random;

    private void Start()
    {
        _startPos = _player.transform.position;
        _score = GetComponent<Score>();
        _timer = GetComponent<Timer>();
        _random = GetComponent<RandomWithSeed>();

        _fsm = new FsmGame();

        _fsm.AddState(new Menu(_fsm, _menuUI, _gameplayUI, _player, _startPos));
        _fsm.AddState(new StartGame(_fsm,  _gameplayUI, _score, _playerHealthSystem, _obstacleContainer, _playerStaminaSystem, _timer, _obstacleManager, _random));
        _fsm.AddState(new Play(_fsm, _gameplayUI, _scoreText, _score, _pauseGameBttn, _timer));
        _fsm.AddState(new Pause(_fsm, _pauseUI, _scoreText, _score, _player, _timer, _obstacleManager));
        _fsm.AddState(new GameOver(_fsm, _gameOverUI, _score, _timer, _obstacleManager));

        _fsm.SetState<Menu>();
    }

    private void Update() => _fsm.Update();

    public void SetMenuState() => _fsm.SetState<Menu>();
    public void SetPauseState() => _fsm.SetState<Pause>();
    public void SetPlayState() => _fsm.SetState<Play>();
    public void SetGameOverState() => _fsm.SetState<GameOver>();
}
