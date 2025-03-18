// EventManager.cs
// Manages centralized event handling, allowing different parts of the game to communicate
// using an event-driven architecture.

using System;

public class EventManager
{
    private static EventManager _instance; // Singleton instance of EventManager
    public static EventManager Instance => _instance ??= new EventManager(); // Lazy initialization of the singleton

    // Event triggered when the player's score is updated (int represents the new score)
    public event Action<int> OnScoreUpdated;

    // Event triggered when the player loses a life
    public event Action OnLifeLost;

    // Event triggered when the game state changes (string represents the new game state)
    public event Action<string> OnGameStateChanged;

    // Triggers the OnScoreUpdated event, notifying all listeners of the new score
    public void TriggerScoreUpdate(int newScore)
    {
        OnScoreUpdated?.Invoke(newScore);
    }

    // Triggers the OnLifeLost event, notifying all listeners that the player lost a life
    public void TriggerLifeLost()
    {
        OnLifeLost?.Invoke();
    }

    // Triggers the OnGameStateChanged event, notifying all listeners of a game state change
    public void TriggerGameStateChange(string newState)
    {
        OnGameStateChanged?.Invoke(newState);
    }
}
