using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameStateManager
{
    private GameState _currentState;

    public void SetState(GameState newState)
    {
        _currentState?.Exit(); // Call Exit() on the current state before switching
        _currentState = newState;
        _currentState.Enter(); // Call Enter() on the new state
    }

    public void Update(GameTime gameTime)
    {
        _currentState?.Update(gameTime); // Call Update() on the current state if it exists
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        _currentState?.Draw(gameTime, spriteBatch); // Delegate draw call to the active state
    }
}