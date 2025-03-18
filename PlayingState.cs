// PlayingState.cs
// Manages the gameplay state, including player movement, spawning objects, and collision handling.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

// Ensure we use the correct SpriteBatch from MonoGame
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

public class PlayingState : GameState
{
    // ✅ Class-level variable declarations (Fix for missing variables)
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

    // ✅ Corrected Constructor with Proper Inheritance
    public PlayingState(GameStateManager stateManager, GraphicsDevice graphicsDevice, ContentManager content)
        : base(stateManager, graphicsDevice, content) { }

    // ✅ Overrides base Enter() correctly
    public override void Enter()
    {
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

        // ✅ Initialize object spawner with movement boundaries
        _spawner = new ObjectSpawner(_ballTexture, _debuffTexture, _graphicsDevice.Viewport.Width, minY, maxY);

        // ✅ Initialize scoring, UI, and collision management
        _scoreManager = new ScoreManager();
        _uiManager = new UIManager(_countdownFont, _scoreManager);
        _collisionManager = new CollisionManager(_player, _scoreManager, _uiManager, _spawner.GetGameObjects());
    }

    // ✅ Overrides base Update() correctly
    public override void Update(GameTime gameTime)
    {
        if (!_gameStarted)
        {
            // Countdown logic before the game starts
            _countdown -= gameTime.ElapsedGameTime.TotalSeconds;
            if (_countdown <= 0)
            {
                _gameStarted = true;
            }
        }
        else
        {
            // Update player movement
            _player.Update(gameTime);

            // Update object spawning
            _spawner.Update(gameTime);

            // Update collision detection
            _collisionManager.Update();
        }
    }

    // ✅ Overrides base Draw() correctly
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // Draw background
        spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height), Color.White);

        if (!_gameStarted)
        {
            // Display countdown in the center of the screen
            string countdownText = _countdown > 1 ? Math.Ceiling(_countdown).ToString() : "GO!";
            Vector2 textSize = _countdownFont.MeasureString(countdownText);
            Vector2 textPosition = new Vector2(
                (_graphicsDevice.Viewport.Width - textSize.X) / 2,
                (_graphicsDevice.Viewport.Height - textSize.Y) / 2
            );

            // Draw countdown timer
            spriteBatch.DrawString(_countdownFont, countdownText, textPosition, Color.White, 0f, Vector2.Zero, 5f, SpriteEffects.None, 0f);
        }
        else
        {
            // Draw player
            _player.Draw(spriteBatch);

            // Draw spawned balls and debuffs
            _spawner.Draw(spriteBatch);

            // ✅ Draw UI (Score & Life Counter)
            _uiManager.Draw(spriteBatch, _graphicsDevice);
        }
    }
}
