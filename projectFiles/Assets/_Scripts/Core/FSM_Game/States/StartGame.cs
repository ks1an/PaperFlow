using UnityEngine;

public class StartGame : FsmGameState
{
    GameObject _gameplayUI;
    Score _score;
    Timer _timer;
    RandomWithSeed _random;
    ObstaclesContainer _obstacleContainer;
    ObstacleManager _obstaclesManager;

    Health _playerHP;
    Stamina _playerStamina;

    public StartGame(FsmGame fsmUI, GameObject gameplayUI, Score score, Health playerHP, ObstaclesContainer obstacleManager, Stamina playerStamina, 
        Timer timer, ObstacleManager obstaclesManager, RandomWithSeed random) : base(fsmUI)
    {
        _gameplayUI = gameplayUI;
        _score = score;
        _playerHP = playerHP;
        _obstacleContainer = obstacleManager;
        _playerStamina = playerStamina;
        _timer = timer;
        _obstaclesManager = obstaclesManager;
        _random = random;
    }

    public override void Enter()
    {
        base.Enter();

        _obstacleContainer.DestoryAllObstacles();
        _random.GetFirstSeed();
        _obstaclesManager.SetStartComplexity();

        _playerHP.HealthToMax();
        _playerStamina.SetStandartStamina();
        _score.SetCurrentScoreToZero();
        _timer.StartTimer();

        _gameplayUI.SetActive(true);

        _obstaclesManager.StartSpawning();

        Fsm.SetState<Play>();
    }
}