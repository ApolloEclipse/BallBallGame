// ScoreManager.cs
// Manages score tracking and triggers events when the score is updated.

public class ScoreManager
{
    private int _score; // Stores the player's current score

    // Initializes the score manager with a starting score of 0
    public ScoreManager()
    {
        _score = 0;
    }

    // Increases the player's score by a given amount and triggers a score update event
    public void IncreaseScore(int amount)
    {
        _score += amount;
        EventManager.Instance.TriggerScoreUpdate(_score); // Notify UI and other systems of the score change
    }

    // Returns the current score
    public int GetScore()
    {
        return _score;
    }
}
