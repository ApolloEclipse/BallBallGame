// GameState.cs
// Defines the base class for all game states, providing structure for transitions, updates, and rendering.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

// Abstract base class for all game states, ensuring consistent behavior across different states.
public abstract class GameState
{
    protected GameStateManager _stateManager; // Reference to the game state manager
    protected GraphicsDevice _graphicsDevice; // Handles rendering operations
    protected ContentManager _content; // Manages and loads game assets

    // Constructor initializes the state with references to core game components
    public GameState(GameStateManager stateManager, GraphicsDevice graphicsDevice, ContentManager content)
    {
        _stateManager = stateManager;
        _graphicsDevice = graphicsDevice;
        _content = content;
    }

    // Called when the state is entered, allowing setup or initialization
    public abstract void Enter();

    // Called when the state is exited, allowing cleanup if needed
    public virtual void Exit() { }

    // Updates the game state logic, ensuring each derived state implements behavior updates
    public abstract void Update(GameTime gameTime);

    // Draws the game state content, ensuring each state implements its rendering logic
    public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
}
