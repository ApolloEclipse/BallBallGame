// IFileHandler.cs
// Defines an interface for handling file operations related to reading and writing scores.

using System.Collections.Generic;

public interface IFileHandler
{
    // Reads scores from a file and returns a list of ScoreEntry objects.
    List<ScoreEntry> ReadScores();

    // Writes the provided list of scores to a file.
    void WriteScores(List<ScoreEntry> scores);

    // Ensures the score file exists, creating it if necessary.
    void EnsureFileExists();
}
