using UnityEngine;

public class Score : MonoBehaviour
{
    public int CurrentScore { get; private set; }
    public int RecordScore { get; private set; }

    public void IncreaseScore(int value)
    {
        if (value < 0)
        {
            DebuginggManager.DebugLogError("Trying to increase the count by a negative number");

            return;
        }

        CurrentScore += value;
    }
    public void SetNewRecord() => RecordScore = CurrentScore;
    public void SetCurrentScoreToZero() => CurrentScore = 0;
}
