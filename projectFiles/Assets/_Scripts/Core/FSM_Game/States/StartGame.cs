using UnityEngine;

public class StartGame : FsmGameState
{
    GameObject _gameplayUI;
    Score _score;
    Timer _timer;
    ObstaclesContainer _obstacleContainer;
    ObstaclesManager _obstaclesManager;

    Health _playerHP;
    Stamina _playerStamina;

    public StartGame(FsmGame fsmUI, GameObject gameplayUI, Score score, Health playerHP, ObstaclesContainer obstacleManager, Stamina playerStamina, Timer timer, ObstaclesManager obstaclesManager) : base(fsmUI)
    {
        _gameplayUI = gameplayUI;
        _score = score;
        _playerHP = playerHP;
        _obstacleContainer = obstacleManager;
        _playerStamina = playerStamina;
        _timer = timer;
        _obstaclesManager = obstaclesManager;
    }

    public override void Enter()
    {
        base.Enter();

        _obstacleContainer.DestoryAllObstacles();
        _obstaclesManager.SetStartComplexity();

        _playerHP.HealthToMax();
        _playerStamina.SetStandartStamina();
        _score.SetCurrentScoreToZero();
        _timer.StartTimer();

        _gameplayUI.SetActive(true);

        Fsm.SetState<Play>();
    }
}