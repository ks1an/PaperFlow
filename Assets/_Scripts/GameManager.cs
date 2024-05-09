using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool IsPlayingRound = false;

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private ObstaclesContainerManager _obstacleManager;

    [Header("Player")]
    [SerializeField] private GameObject _player;
    [SerializeField] private Vector2 _startPos;
    [SerializeField] private HealthSystem _playerHealth;

    private int _score = 0;
    private int _recordScore = 0;

    private void Start()
    {
        IsPlayingRound = false;
        FocusBackgroundPanel.FocusBackPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (!IsPlayingRound)
            if (Input.GetKeyDown(KeyCode.Space))
                GameStart();

        if (_playerHealth.CurrentHelth == 0)
            GameOver();
    }

    public void GameOver()
    {
        IsPlayingRound = false;

        if (DebuginggManager.CanDebugging)
            DebuginggManager.DebugLog($"Game Over. Score: {_score}");

        Time.timeScale = 0f;
        FocusBackgroundPanel.FocusBackPanel.SetActive(true);

        if (_score > _recordScore)
        {
            if (DebuginggManager.CanDebugging)
                DebuginggManager.DebugLog($"New record! Your new record: {_score}. Your previous record: {_recordScore}.");

            _recordScore = _score;
        }
    }

    public void GameStart()
    {
        IsPlayingRound = true;

        _playerHealth.HealthToMax();
        _player.transform.position = _startPos;
        _obstacleManager.DestoryAllObstacles();

        _score = 0;
        _scoreText.text = _score.ToString();
        FocusBackgroundPanel.FocusBackPanel.SetActive(false);
        Time.timeScale = 1f;

        if (DebuginggManager.CanDebugging)
            DebuginggManager.DebugLog($"Game Start!");
    }

    public void IncreaseScore(int value)
    {
        if (value < 0)
        {
            if (DebuginggManager.CanDebugging)
                DebuginggManager.DebugLogError("Trying to increase the count by a negative number");

            return;
        }

        _score += value;
        _scoreText.text = _score.ToString();
    }
}
