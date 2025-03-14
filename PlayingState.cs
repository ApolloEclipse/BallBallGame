using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class PlayingState : GameState
{
    private Texture2D _backgroundTexture;  // Background image
    private Texture2D _playerTexture;      // Player image
    private Vector2 _playerStartPosition;  // Initial player position
    private Player _player;                // Player object
    private SpriteFont _countdownFont;     // Font for countdown timer
    private double _countdown = 3.0;       // Countdown timer in seconds
    private bool _gameStarted = false;     // Indicates if game has started

    public PlayingState(GameStateManager stateManager, GraphicsDevice graphicsDevice, ContentManager content)
        : base(stateManager, graphicsDevice, content)
    {
    }

    public override void Enter()
    {
        // Load assets
        _backgroundTexture = _content.Load<Texture2D>("UI/Images/BackgroundGame");
        _playerTexture = _content.Load<Texture2D>("Textures/player_ball");
        _countdownFont = _content.Load<SpriteFont>("UI/Fonts/BebasNeueFont");

        // Set player start position
        _playerStartPosition = new Vector2(
            _graphicsDevice.Viewport.Width * 0.1f, // 10% from the left
            _graphicsDevice.Viewport.Height * 0.8f // 80% from the top
        );

        // Define movement boundaries
        int minY = (int)(_graphicsDevice.Viewport.Height * 0.45f); // 45% from the top
        int maxY = (int)(_graphicsDevice.Viewport.Height * 0.8f); // 80% from the top

        // Initialize player
        _player = new Player(_playerTexture, _playerStartPosition, minY, maxY);
    }

    public override void Update(GameTime gameTime)
    {
        if (!_gameStarted)
        {
            // Countdown logic
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
        }
    }

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

            // Countdown Timer Font Size
            spriteBatch.DrawString(_countdownFont, countdownText, textPosition, Color.White, 0f, Vector2.Zero, 5f, SpriteEffects.None, 0f);

        }
        else
        {
            // Draw player after countdown
            _player.Draw(spriteBatch);
        }
    }
}
