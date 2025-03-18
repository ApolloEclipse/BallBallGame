// ScoreEntry.cs
// Represents a player's score entry for the leaderboard.

public class ScoreEntry
{
    public string PlayerName { get; set; } // Stores the player's name
    public int Score { get; set; } // Stores the player's score

    // Constructor initializes a score entry with the player's name and score
    public ScoreEntry(string playerName, int score)
    {
        PlayerName = playerName;
        Score = score;
    }
}
