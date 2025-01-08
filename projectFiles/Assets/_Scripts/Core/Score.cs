using TMPro;
using UnityEngine;

public sealed class Score : MonoBehaviour
{
    public static int CurrentScore { get; private set; }
    public int RecordScore { get; private set; }

    [SerializeField] TextMeshProUGUI _scoreText;

    public void IncreaseScore(int value)
    {
        if (value < 0)
        {
            Debug.LogError("Trying to increase the count by a negative number");
            return;
        }

        CurrentScore += value;
        _scoreText.text = CurrentScore.ToString();
    }

    #region Set

    void SetScoreTextOnPlay() => _scoreText.text = CurrentScore.ToString();

    void SetScoreTextOnPause() => _scoreText.text = $"{CurrentScore}/{RecordScore}";

    public void TrySetNewRecord()
    {
        if (CurrentScore > RecordScore)
        {
            RecordScore = CurrentScore;
        }
    }

    void SetCurrentScoreToZero() => CurrentScore = 0;
    #endregion

    private void OnEnable()
    {
        #region Subscribe

        GameStateController.OnStartProcedureGameState += SetCurrentScoreToZero;
        GameStateController.onPauseState += SetScoreTextOnPause;
        GameStateController.onPlayState += SetScoreTextOnPlay;
        GameStateController.onGameOverState += TrySetNewRecord;
        #endregion
    }
    private void OnDisable()
    {
        #region UnSubscribe

        GameStateController.OnStartProcedureGameState -= SetCurrentScoreToZero;
        GameStateController.onPauseState -= SetScoreTextOnPause;
        GameStateController.onPlayState -= SetScoreTextOnPlay;
        GameStateController.onGameOverState -= TrySetNewRecord;
        #endregion
    }
}
