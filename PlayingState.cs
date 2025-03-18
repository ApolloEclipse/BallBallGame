// PlayingState.cs
// Manages gameplay, player movement, spawning, collision handling, and game over detection.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

public class PlayingState : GameState
{
    private Texture2D _backgroundTexture;
    private Texture2D _playerTexture;
    private Texture2D _ballTexture;
    private Texture2D _debuffTexture;

    private Vector2 _playerStartPosition;
    private Player _player;
    private SpriteFont _countdownFont;

    private ObjectSpawner _spawner;
    private ScoreManager _scoreManager;
    private UIManager _uiManager;
    private CollisionManager _collisionManager;

    private double _countdown = 3.0;
    private bool _gameStarted = false;
    private int _lives = 3; // ✅ Life counter for game over logic.

    private double _elapsedTime = 0; // ✅ Timer to track elapsed time.

    public PlayingState(GameStateManager stateManager, GraphicsDevice graphicsDevice, ContentManager content)
        : base(stateManager, graphicsDevice, content) { }

    public override void Enter()
    {
        _lives = 3; // ✅ Reset lives when entering PlayingState
        _elapsedTime = 0; // ✅ Reset timer

        // Load assets
        _backgroundTexture = _content.Load<Texture2D>("UI/Images/BackgroundGame");
        _playerTexture = _content.Load<Texture2D>("Textures/player_ball");
        _ballTexture = _content.Load<Texture2D>("Textures/Ball");
        _debuffTexture = _content.Load<Texture2D>("Textures/Debuff");
        _countdownFont = _content.Load<SpriteFont>("UI/Fonts/BebasNeueFont");

        // Set player start position
        _playerStartPosition = new Vector2(
            _graphicsDevice.Viewport.Width * 0.1f,
            _graphicsDevice.Viewport.Height * 0.8f
        );

        // Define player movement boundaries
        int minY = (int)(_graphicsDevice.Viewport.Height * 0.45f);
        int maxY = (int)(_graphicsDevice.Viewport.Height * 0.8f);

        // Initialize player
        _player = new Player(_playerTexture, _playerStartPosition, minY, maxY);

        // Initialize object spawner
        _spawner = new ObjectSpawner(_ballTexture, _debuffTexture, _graphicsDevice.Viewport.Width, minY, maxY);

        // Initialize scoring, UI, and collision management
        _scoreManager = new ScoreManager();

        // ✅ Ensure UIManager is properly reset
        _uiManager?.Unsubscribe();
        _uiManager = new UIManager(_countdownFont, _scoreManager);

        _collisionManager = new CollisionManager(_player, _scoreManager, _uiManager, this, _spawner.GetGameObjects());

        // ✅ Prevent duplicate event subscriptions
        EventManager.Instance.OnLifeLost -= ReduceLife;
        EventManager.Instance.OnLifeLost += ReduceLife;
    }

    public override void Exit()
    {
        System.Diagnostics.Debug.WriteLine("Exiting PlayingState: Unsubscribing from events.");

        // ✅ Ensure event is fully removed before switching state
        EventManager.Instance.OnLifeLost -= ReduceLife;

        // ✅ Also clear the UIManager events before exiting
        _uiManager?.Unsubscribe();
    }

    public override void Update(GameTime gameTime)
    {
        if (!_gameStarted)
        {
            _countdown -= gameTime.ElapsedGameTime.TotalSeconds;
            if (_countdown <= 0)
            {
                _gameStarted = true;
                _countdown = 0;
            }
        }
        else
        {
            _elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            _player.Update(gameTime);
            _spawner.Update(gameTime);
            _collisionManager.Update();

            if (_lives <= 0)
            {
                System.Diagnostics.Debug.WriteLine("LIVES == 0: TRANSITIONING TO GAME OVER");
                _stateManager.SetState(new GameOverState(_stateManager, _graphicsDevice, _content, _scoreManager.GetScore()));
            }
        }
    }

    public void ReduceLife()
    {
        System.Diagnostics.Debug.WriteLine($"ReduceLife() called. Current Lives: {_lives}");

        if (_lives > 0)
        {
            _lives--;
            System.Diagnostics.Debug.WriteLine($"Life lost! Remaining lives: {_lives}");
        }

        if (_lives <= 0) // Ensure it only transitions once
        {
            System.Diagnostics.Debug.WriteLine("Lives == 0: Switching to GameOverState!");
            _stateManager.SetState(new GameOverState(_stateManager, _graphicsDevice, _content, _scoreManager.GetScore()));
        }
    }


    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height), Color.White);

        if (!_gameStarted)
        {
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
            _player.Draw(spriteBatch);
            _spawner.Draw(spriteBatch);
            _uiManager.Draw(spriteBatch, _graphicsDevice);

            // ✅ Draw the Timer inside the red UI box
            string timerText = FormatTime(_elapsedTime);
            Vector2 timerSize = _countdownFont.MeasureString(timerText);

            // Define the red box position (based on UI layout)
            Vector2 redBoxPosition = new Vector2(80, 40); // Adjust based on your UI
            Vector2 timerPosition = new Vector2(
                redBoxPosition.X + (200 - timerSize.X) / 2, // Centered inside the box width
                redBoxPosition.Y + (50 - timerSize.Y) / 2   // Centered inside the box height
            );

            spriteBatch.DrawString(_countdownFont, timerText, timerPosition, Color.White);
        }
    }

    private string FormatTime(double timeInSeconds)
    {
        int minutes = (int)(timeInSeconds / 60);
        int seconds = (int)(timeInSeconds % 60);
        int milliseconds = (int)((timeInSeconds - Math.Floor(timeInSeconds)) * 100);

        return $"{minutes:D2}:{seconds:D2}:{milliseconds:D2}";
    }
}
