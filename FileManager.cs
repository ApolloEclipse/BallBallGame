// FileManager.cs
// Handles saving and loading of scores using JSON format.

using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json; // Ensure Newtonsoft.Json is used for serialization

public class FileManager
{
    private string _filePath; // Path for the scores file

    // Constructor initializes the file path for storing scores.
    public FileManager(string filePath)
    {
        _filePath = filePath;

        // Ensure the file exists to avoid errors on the first read
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]"); // Initialize with an empty JSON array
        }
    }

    // Saves the list of scores to the file in JSON format.
    public void SaveScores(List<ScoreEntry> scores)
    {
        try
        {
            string json = JsonConvert.SerializeObject(scores, Newtonsoft.Json.Formatting.Indented); // Fix ambiguity
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving scores: {ex.Message}"); // Log error to console (for debugging)
        }
    }

    // Loads the list of scores from the file.
    public List<ScoreEntry> LoadScores()
    {
        try
        {
            string json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<ScoreEntry>>(json) ?? new List<ScoreEntry>(); // Ensure non-null return
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading scores: {ex.Message}");
            return new List<ScoreEntry>(); // Return empty list on failure
        }
    }
}
