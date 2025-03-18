// ScoreEntry.cs
// Stores player name & score for leaderboard

public class ScoreEntry
{
    public string PlayerName { get; set; }
    public int Score { get; set; }

    public ScoreEntry(string playerName, int score)
    {
        PlayerName = playerName;
        Score = score;
    }
}
