using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int CurrentScore { get; private set; }
    public int RecordScore { get; private set; }

    [SerializeField] TextMeshProUGUI _scoreText;

    public void IncreaseScore(int value)
    {
        if (value < 0)
        {
            DebuginggManager.LogError("Trying to increase the count by a negative number");

            return;
        }

        CurrentScore += value;
        _scoreText.text = CurrentScore.ToString();
    }

    void SetScoreTextOnPlay() => _scoreText.text = CurrentScore.ToString();

    void SetScoreTextOnPause() => _scoreText.text = $"{CurrentScore}/{RecordScore}";

    public void TrySetNewRecord()
    {
        if(CurrentScore > RecordScore)
        {
            RecordScore = CurrentScore;
            DebuginggManager.Log($"New record! Your new record: {CurrentScore}. Your previous record: {RecordScore}.");
        }
    }

    void SetCurrentScoreToZero() => CurrentScore = 0;

    private void OnEnable()
    {
        GameController.onStartGameState += SetCurrentScoreToZero;
        GameController.onPauseState += SetScoreTextOnPause;
        GameController.onPlayState += SetScoreTextOnPlay;
        GameController.onGameOverState += TrySetNewRecord;
    }
    private void OnDisable()
    {
        GameController.onStartGameState -= SetCurrentScoreToZero;
        GameController.onPauseState -= SetScoreTextOnPause;
        GameController.onPlayState -= SetScoreTextOnPlay;
        GameController.onGameOverState -= TrySetNewRecord;
    }
}
