// ScoreManager.cs
// Handles score tracking and emits events for UI updates.

public class ScoreManager
{
    private int _score;

    public ScoreManager()
    {
        _score = 0;
    }

    public void IncreaseScore(int amount)
    {
        _score += amount;
        EventManager.Instance.TriggerScoreUpdate(_score);
    }

    public int GetScore()
    {
        return _score;
    }
}
