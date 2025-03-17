using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

// Abstract base class for all game states
public abstract class GameState
{
    protected GameStateManager _stateManager; // Manages game states
    protected GraphicsDevice _graphicsDevice; // Handles rendering
    protected ContentManager _content; // Loads content

    public GameState(GameStateManager stateManager, GraphicsDevice graphicsDevice, ContentManager content)
    {
        _stateManager = stateManager;
        _graphicsDevice = graphicsDevice;
        _content = content;
    }

    public abstract void Enter(); // Called when state is entered
    public virtual void Exit() { } // Called when state is exited

    public abstract void Update(GameTime gameTime); // Ensures each game state implements logic updates

    public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch); // Handles rendering
}