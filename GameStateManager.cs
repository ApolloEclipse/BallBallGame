using System;
using Microsoft.Xna.Framework;

public class GameStateManager
{
    private GameState _currentState; // Stores the active game state

    // Switches to a new game state
    public void SetState(GameState newState)
    {
        _currentState?.Exit(); // Call Exit on the previous state (if any), (?.) is a null-conditional operator and its a safety check to prevent NullReferenceException
        _currentState = newState;
        _currentState.Enter(); // Initialize the new state
    }

    // Calls Update() on the current state
    public void Update(GameTime gameTime)
    {
        _currentState?.Update(gameTime); // Update the active state
    }

    // Calls Draw() on the current state
    public void Draw(GameTime gameTime)
    {
        _currentState?.Draw(gameTime); // Draw the active state
    }
}
