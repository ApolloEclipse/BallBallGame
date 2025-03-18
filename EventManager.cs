// EventManager.cs
// Centralized event management system for event-driven architecture.

using System;

public class EventManager
{
    private static EventManager _instance;
    public static EventManager Instance => _instance ??= new EventManager();

    // Define events using delegates
    public event Action<int> OnScoreUpdated;
    public event Action OnLifeLost;
    public event Action<string> OnGameStateChanged;

    public void TriggerScoreUpdate(int newScore)
    {
        OnScoreUpdated?.Invoke(newScore);
    }

    public void TriggerLifeLost()
    {
        OnLifeLost?.Invoke();
    }

    public void TriggerGameStateChange(string newState)
    {
        OnGameStateChanged?.Invoke(newState);
    }
}
