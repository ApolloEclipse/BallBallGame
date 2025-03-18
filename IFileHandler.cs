// IFileHandler.cs
// Defines file operations: Read, Write, and Ensure File Exists

using System.Collections.Generic;

public interface IFileHandler
{
    List<ScoreEntry> ReadScores();
    void WriteScores(List<ScoreEntry> scores);
    void EnsureFileExists();
}
