using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class GameManager : Game
{
    // Handles graphics rendering
    private GraphicsDeviceManager _graphics; // Handles window size, resolution, and graphics settings
    private SpriteBatch _spriteBatch;        // Draw 2D images (sprites)

    // Player texture & position
    private Texture2D _playerTexture; // Player sprite
    private Vector2 _playerPosition;  // player x,y position
    private Vector2 _playerVelocity;  // player movement speed

    // Input handling (detect key presses)
    private KeyboardState _currentKeyState;  // Current frame’s keyboard input
    private KeyboardState _previousKeyState; // Previous frame’s keyboard input

    // Player boundary
    private float minY; // Upper boundary
    private float maxY; // Lower boundary

    public GameManager()
    {
        // Initialize graphics
        _graphics = new GraphicsDeviceManager(this); // Initializes graphics settings
        Content.RootDirectory = "Content";       // Sets the content root directory (Content/) where assets (textures, sounds) are loaded from

        // Set window size to 1080p
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        // Set initial player position & movement (center of the screen)
        _playerPosition = new Vector2(960, 540); // Position the player in the center
        _playerVelocity = new Vector2(0, 5);     // Starts moving up & down - movement velocity is 5 pixels per frame

        // Define movement boundaries
        minY = 150;                                       // Upper limit - Y = 150p
        maxY = _graphics.PreferredBackBufferHeight - 400; // Lower limit - Y = 680p

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Load player texture
        _playerTexture = Content.Load<Texture2D>("Textures/player_ball"); // player sprite
    }

    protected override void Update(GameTime gameTime)
    {
        // Get the keyboard state
        _currentKeyState = Keyboard.GetState();

        // If the player presses SPACE, change direction
        if (_currentKeyState.IsKeyDown(Keys.Space) && _previousKeyState.IsKeyUp(Keys.Space))
        {
            _playerVelocity.Y = -_playerVelocity.Y; // Flip vertical movement
        }

        // Update player position
        _playerPosition += _playerVelocity;

        // Prevent moving outside custom boundaries
        if (_playerPosition.Y < minY) _playerPosition.Y = minY;
        if (_playerPosition.Y > maxY) _playerPosition.Y = maxY;

        // Store the previous key state for edge detection
        _previousKeyState = _currentKeyState;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue); // Clears the screen with a blue background

        _spriteBatch.Begin();

        // Draw the player with scaling to fit higher resolution
        _spriteBatch.Draw(_playerTexture, _playerPosition, null, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);

        _spriteBatch.End(); // Ends the draw

        base.Draw(gameTime);
    }
}
