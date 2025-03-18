// ScoreManager.cs
// Manages the player's score.

public class ScoreManager
{
    private int _score;

    public ScoreManager()
    {
        _score = 0;
    }

    public void AddScore(int value)
    {
        _score += value;
    }

    public int GetScore()
    {
        return _score;
    }
}
