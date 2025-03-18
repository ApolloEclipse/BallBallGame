// GameManager.cs
// Manages the core game loop, initializes graphics, loads content, and handles game state transitions.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

public class GameManager : Game
{
    private GraphicsDeviceManager _graphics; // Handles graphics settings and resolution
    private SpriteBatch _spriteBatch; // Used for rendering game objects
    private GameStateManager _stateManager; // Manages game states

    // Constructor initializes the graphics settings and game properties
    public GameManager()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true; // Show mouse cursor during gameplay

        // Set the game window resolution to Full HD (1920x1080)
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.ApplyChanges();
    }

    // Loads game content such as textures and initializes the main game state
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice); // Create a SpriteBatch for rendering
        _stateManager = new GameStateManager(); // Initialize the game state manager

        // Set the initial game state to the main menu
        _stateManager.SetState(new MainMenuState(_stateManager, GraphicsDevice, Content));
    }

    // Updates the game logic, such as game state updates, player input, and physics
    protected override void Update(GameTime gameTime)
    {
        _stateManager.Update(gameTime); // Update the active game state
        base.Update(gameTime);
    }

    // Draws the current game frame
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue); // Clear the screen with a default color

        _spriteBatch.Begin(); // Begin rendering all drawable elements

        _stateManager.Draw(gameTime, _spriteBatch); // Delegate rendering to the active game state

        _spriteBatch.End(); // End the rendering batch

        base.Draw(gameTime);
    }
}
