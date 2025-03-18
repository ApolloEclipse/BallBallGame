// GameStateManager.cs
// Manages transitions between different game states and triggers events when states change.

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class GameStateManager
{
    private GameState _currentState;
    private Stack<GameState> _stateStack;

    public GameStateManager()
    {
        _stateStack = new Stack<GameState>();
    }

    public void SetState(GameState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();

        // ✅ Fire event with string identifier
        EventManager.Instance.TriggerGameStateChange(newState.GetType().Name);
    }

    public void PushState(GameState newState)
    {
        _stateStack.Push(_currentState);
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();

        // ✅ Fire event with string identifier
        EventManager.Instance.TriggerGameStateChange(newState.GetType().Name);
    }

    public void PopState()
    {
        if (_stateStack.Count > 0)
        {
            _currentState?.Exit();
            _currentState = _stateStack.Pop();
            _currentState.Enter();

            // ✅ Fire event with string identifier
            EventManager.Instance.TriggerGameStateChange(_currentState.GetType().Name);
        }
    }

    public void Update(GameTime gameTime)
    {
        _currentState?.Update(gameTime);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        _currentState?.Draw(gameTime, spriteBatch);
    }
}
