using UnityEngine;

public class StartGame : FsmGameState
{
    private GameObject _gameplayUI;
    private Score _score;
    private ObstaclesContainerManager _obstacleManager;

    private Health _playerHP;
    private Stamina _playerStamina;

    public StartGame(FsmGame fsmUI, GameObject gameplayUI, Score score, Health playerHP, ObstaclesContainerManager obstacleManager, Stamina playerStamina) : base(fsmUI)
    {
        _gameplayUI = gameplayUI;
        _score = score;
        _playerHP = playerHP;
        _obstacleManager = obstacleManager;
        _playerStamina = playerStamina;
    }

    public override void Enter()
    {
        base.Enter();

        _obstacleManager.DestoryAllObstacles();
        _score.SetCurrentScoreToZero();
        _playerHP.HealthToMax();
        _playerStamina.SetStandartStamina();

        _gameplayUI.SetActive(true);

        Fsm.SetState<Play>();
    }
}