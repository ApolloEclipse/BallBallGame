// PlayingState.cs
// Manages gameplay, including player movement, object spawning, collisions, and game-over conditions.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

public class PlayingState : GameState
{
    private Texture2D _backgroundTexture; // Background texture for gameplay
    private Texture2D _playerTexture; // Texture for the player character
    private Texture2D _ballTexture; // Texture for collectible balls
    private Texture2D _debuffTexture; // Texture for harmful obstacles

    private Vector2 _playerStartPosition; // Initial player position
    private Player _player; // Player object
    private SpriteFont _countdownFont; // Font for countdown and timer display

    private ObjectSpawner _spawner; // Manages object spawning
    private ScoreManager _scoreManager; // Handles score tracking
    private UIManager _uiManager; // Manages UI elements
    private CollisionManager _collisionManager; // Handles collision detection

    private double _countdown = 3.0; // Countdown before game starts
    private bool _gameStarted = false; // Tracks whether the game has started
    private int _lives = 3; // Number of lives the player has

    private double _elapsedTime = 0; // Timer to track total game time

    // Constructor initializes the playing state
    public PlayingState(GameStateManager stateManager, GraphicsDevice graphicsDevice, ContentManager content)
        : base(stateManager, graphicsDevice, content) { }

    // Loads assets and initializes objects when the game state is entered
    public override void Enter()
    {
        _lives = 3; // Reset lives
        _elapsedTime = 0; // Reset timer

        // Load textures and fonts
        _backgroundTexture = _content.Load<Texture2D>("UI/Images/BackgroundGame");
        _playerTexture = _content.Load<Texture2D>("Textures/player_ball");
        _ballTexture = _content.Load<Texture2D>("Textures/Ball");
        _debuffTexture = _content.Load<Texture2D>("Textures/Debuff");
        _countdownFont = _content.Load<SpriteFont>("UI/Fonts/BebasNeueFont");

        // Set initial player position
        _playerStartPosition = new Vector2(
            _graphicsDevice.Viewport.Width * 0.1f,
            _graphicsDevice.Viewport.Height * 0.8f
        );

        // Define movement boundaries for the player
        int minY = (int)(_graphicsDevice.Viewport.Height * 0.45f);
        int maxY = (int)(_graphicsDevice.Viewport.Height * 0.8f);

        // Initialize player
        _player = new Player(_playerTexture, _playerStartPosition, minY, maxY);

        // Initialize object spawner
        _spawner = new ObjectSpawner(_ballTexture, _debuffTexture, _graphicsDevice.Viewport.Width, minY, maxY);

        // Initialize score manager and UI
        _scoreManager = new ScoreManager();

        // Ensure UIManager is properly reset before assigning a new one
        _uiManager?.Unsubscribe();
        _uiManager = new UIManager(_countdownFont, _scoreManager);

        // Initialize collision manager with references to game objects
        _collisionManager = new CollisionManager(_player, _scoreManager, _uiManager, this, _spawner.GetGameObjects());

        // Prevent duplicate event subscriptions for life loss
        EventManager.Instance.OnLifeLost -= ReduceLife;
        EventManager.Instance.OnLifeLost += ReduceLife;
    }

    // Cleans up resources and event subscriptions when exiting the playing state
    public override void Exit()
    {
        System.Diagnostics.Debug.WriteLine("Exiting PlayingState: Unsubscribing from events.");

        // Ensure event is fully removed before switching states
        EventManager.Instance.OnLifeLost -= ReduceLife;

        // Unsubscribe UIManager events to prevent memory leaks
        _uiManager?.Unsubscribe();
    }

    // Updates game logic, including countdown, movement, collisions, and game-over conditions
    public override void Update(GameTime gameTime)
    {
        if (!_gameStarted)
        {
            // Update countdown before starting the game
            _countdown -= gameTime.ElapsedGameTime.TotalSeconds;
            if (_countdown <= 0)
            {
                _gameStarted = true;
                _countdown = 0;
            }
        }
        else
        {
            // Update elapsed time
            _elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            // Update game elements
            _player.Update(gameTime);
            _spawner.Update(gameTime);
            _collisionManager.Update();

            // Check if the player has lost all lives
            if (_lives <= 0)
            {
                System.Diagnostics.Debug.WriteLine("LIVES == 0: TRANSITIONING TO GAME OVER");
                _stateManager.SetState(new GameOverState(_stateManager, _graphicsDevice, _content, _scoreManager.GetScore()));
            }
        }
    }

    // Handles reducing the player's lives when hit by a debuff
    public void ReduceLife()
    {
        System.Diagnostics.Debug.WriteLine($"ReduceLife() called. Current Lives: {_lives}");

        if (_lives > 0)
        {
            _lives--;
            System.Diagnostics.Debug.WriteLine($"Life lost! Remaining lives: {_lives}");
        }

        // If no lives remain, transition to the Game Over state
        if (_lives <= 0)
        {
            System.Diagnostics.Debug.WriteLine("Lives == 0: Switching to GameOverState!");
            _stateManager.SetState(new GameOverState(_stateManager, _graphicsDevice, _content, _scoreManager.GetScore()));
        }
    }

    // Draws game elements, including background, player, and UI elements
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height), Color.White);

        if (!_gameStarted)
        {
            // Display countdown before game starts
            string countdownText = _countdown > 1 ? Math.Ceiling(_countdown).ToString() : "GO!";
            Vector2 textSize = _countdownFont.MeasureString(countdownText);
            Vector2 textPosition = new Vector2(
                (_graphicsDevice.Viewport.Width - textSize.X) / 2,
                (_graphicsDevice.Viewport.Height - textSize.Y) / 2
            );

            spriteBatch.DrawString(_countdownFont, countdownText, textPosition, Color.White, 0f, Vector2.Zero, 5f, SpriteEffects.None, 0f);
        }
        else
        {
            // Draw player, spawned objects, and UI
            _player.Draw(spriteBatch);
            _spawner.Draw(spriteBatch);
            _uiManager.Draw(spriteBatch, _graphicsDevice);

            // Display elapsed game time
            string timerText = FormatTime(_elapsedTime);
            Vector2 timerSize = _countdownFont.MeasureString(timerText);

            // Define timer position inside a UI box
            Vector2 redBoxPosition = new Vector2(80, 40);
            Vector2 timerPosition = new Vector2(
                redBoxPosition.X + (200 - timerSize.X) / 2, // Centered in the box
                redBoxPosition.Y + (50 - timerSize.Y) / 2
            );

            spriteBatch.DrawString(_countdownFont, timerText, timerPosition, Color.White);
        }
    }

    // Formats elapsed time into MM:SS:MS format
    private string FormatTime(double timeInSeconds)
    {
        int minutes = (int)(timeInSeconds / 60);
        int seconds = (int)(timeInSeconds % 60);
        int milliseconds = (int)((timeInSeconds - Math.Floor(timeInSeconds)) * 100);

        return $"{minutes:D2}:{seconds:D2}:{milliseconds:D2}";
    }
}
