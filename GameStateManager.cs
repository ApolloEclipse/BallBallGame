// GameStateManager.cs
// Manages transitions between different game states, allowing pushing, popping, and replacing states.
// Triggers events when game states change to notify other systems.

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class GameStateManager
{
    private GameState _currentState; // Stores the currently active game state
    private Stack<GameState> _stateStack; // Stack to allow state transitions (push/pop functionality)

    // Initializes the state manager and creates an empty stack for managing states
    public GameStateManager()
    {
        _stateStack = new Stack<GameState>();
    }

    // Replaces the current game state with a new one
    public void SetState(GameState newState)
    {
        _currentState?.Exit(); // Exit the current state if one exists
        _currentState = newState;
        _currentState.Enter(); // Enter the new state

        // Notify event listeners that the game state has changed
        EventManager.Instance.TriggerGameStateChange(newState.GetType().Name);
    }

    // Pushes a new state onto the stack while keeping the previous state stored
    public void PushState(GameState newState)
    {
        _stateStack.Push(_currentState); // Save the current state
        _currentState?.Exit(); // Exit the current state
        _currentState = newState;
        _currentState.Enter(); // Enter the new state

        // Notify event listeners that the game state has changed
        EventManager.Instance.TriggerGameStateChange(newState.GetType().Name);
    }

    // Pops the current state from the stack, returning to the previous state
    public void PopState()
    {
        if (_stateStack.Count > 0)
        {
            _currentState?.Exit(); // Exit the current state
            _currentState = _stateStack.Pop(); // Restore the previous state
            _currentState.Enter(); // Re-enter the previous state

            // Notify event listeners that the game state has changed
            EventManager.Instance.TriggerGameStateChange(_currentState.GetType().Name);
        }
    }

    // Updates the current state, ensuring the correct game logic is executed
    public void Update(GameTime gameTime)
    {
        _currentState?.Update(gameTime);
    }

    // Draws the current state's content
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        _currentState?.Draw(gameTime, spriteBatch);
    }
}
