using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

public class GameManager : Game
{
    private GraphicsDeviceManager _graphics; // Manages graphics settings
    private SpriteBatch _spriteBatch; // Handles rendering
    private GameStateManager _stateManager;

    public GameManager()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true; // Make mouse pointer visible

        // Set game resolution to 1920x1080 (Full HD)
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.ApplyChanges();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice); // Initialize SpriteBatch
        _stateManager = new GameStateManager();
        _stateManager.SetState(new MainMenuState(_stateManager, GraphicsDevice, Content)); // Start with the main menu
    }

    protected override void Update(GameTime gameTime)
    {
        _stateManager.Update(gameTime); // Update game state manager
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue); // Clear screen

        _spriteBatch.Begin(); // Start rendering once per frame

        _stateManager.Draw(gameTime, _spriteBatch); // Delegate draw call to the active state

        _spriteBatch.End(); // End rendering once per frame

        base.Draw(gameTime);
    }
}
