using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class GameManager : Game
{
    // Handles graphics rendering
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // Player texture & position
    private Texture2D _playerTexture;
    private Vector2 _playerPosition;
    private Vector2 _playerVelocity;

    // Input handling
    private KeyboardState _currentKeyState;
    private KeyboardState _previousKeyState;

    public GameManager()
    {
        // Initialize graphics
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";

        // Set window size to 1080p
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        // Set initial player position & movement (center of the screen)
        _playerPosition = new Vector2(960, 540);
        _playerVelocity = new Vector2(2, 0); // Starts moving to the right

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Load player texture
        _playerTexture = Content.Load<Texture2D>("Textures/player_ball");
    }

    protected override void Update(GameTime gameTime)
    {
        // Get the keyboard state
        _currentKeyState = Keyboard.GetState();

        // If the player presses SPACE, change direction
        if (_currentKeyState.IsKeyDown(Keys.Space) && _previousKeyState.IsKeyUp(Keys.Space))
        {
            _playerVelocity.X = -_playerVelocity.X; // Flip horizontal movement
        }

        // Update player position
        _playerPosition += _playerVelocity;

        // Prevent moving outside screen bounds
        if (_playerPosition.X < 0) _playerPosition.X = 0;
        if (_playerPosition.X > _graphics.PreferredBackBufferWidth - _playerTexture.Width)
            _playerPosition.X = _graphics.PreferredBackBufferWidth - _playerTexture.Width;

        // Store the previous key state for edge detection
        _previousKeyState = _currentKeyState;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        // Draw the player with scaling to fit higher resolution
        _spriteBatch.Draw(_playerTexture, _playerPosition, null, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
