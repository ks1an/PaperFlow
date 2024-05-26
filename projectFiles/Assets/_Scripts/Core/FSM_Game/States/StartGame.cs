using UnityEngine;

public class StartGame : FsmGameState
{
    private GameObject _gameplayUI;
    private Score _score;
    private ObstaclesContainerManager _obstacleManager;

    private HealthSystem _playerHP;

    public StartGame(FsmGame fsmUI, GameObject gameplayUI, Score score, HealthSystem playerHP, ObstaclesContainerManager obstacleManager) : base(fsmUI)
    {
        _gameplayUI = gameplayUI;
        _score = score;
        _playerHP = playerHP;
        _obstacleManager = obstacleManager;
    }

    public override void Enter()
    {
        base.Enter();

        _obstacleManager.DestoryAllObstacles();
        _score.SetCurrentScoreToZero();
        _playerHP.HealthToMax();

        _gameplayUI.SetActive(true);

        Fsm.SetState<Play>();
    }
}