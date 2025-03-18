// FileManager.cs
// Manages saving and loading of player scores using JSON serialization.

using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json; // Used for JSON serialization and deserialization

public class FileManager
{
    private string _filePath; // Path where scores are stored

    // Initializes the FileManager with the provided file path.
    // Ensures the file exists before attempting to read from it.
    public FileManager(string filePath)
    {
        _filePath = filePath;

        // If the scores file does not exist, create an empty JSON file
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
            // Convert the score list to a formatted JSON string
            string json = JsonConvert.SerializeObject(scores, Newtonsoft.Json.Formatting.Indented);

            // Write the JSON string to the file
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            // Log an error message if saving fails
            Console.WriteLine($"Error saving scores: {ex.Message}");
        }
    }

    // Loads the list of scores from the file.
    // Returns an empty list if the file is missing or corrupt.
    public List<ScoreEntry> LoadScores()
    {
        try
        {
            // Read the JSON file content
            string json = File.ReadAllText(_filePath);

            // Deserialize the JSON content into a list of ScoreEntry objects
            return JsonConvert.DeserializeObject<List<ScoreEntry>>(json) ?? new List<ScoreEntry>();
        }
        catch (Exception ex)
        {
            // Log an error message if loading fails
            Console.WriteLine($"Error loading scores: {ex.Message}");

            // Return an empty list to avoid crashes
            return new List<ScoreEntry>();
        }
    }
}
