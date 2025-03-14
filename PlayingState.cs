using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class PlayingState : GameState
{
    private Texture2D _backgroundTexture;
    private Texture2D _playerTexture;
    private Vector2 _playerPosition;
    private SpriteFont _titleFont; // Font for countdown

    private float _countdown = 3.0f; // Countdown starts at 3 seconds
    private bool _gameStarted = false;

    public PlayingState(GameStateManager stateManager, GraphicsDevice graphicsDevice, ContentManager content)
        : base(stateManager, graphicsDevice, content)
    {
    }

    public override void Enter()
    {
        // Load game assets
        _backgroundTexture = _content.Load<Texture2D>("UI/Images/BackgroundGame");
        _playerTexture = _content.Load<Texture2D>("Textures/player_ball");
        _titleFont = _content.Load<SpriteFont>("UI/Fonts/BebasNeueFont"); // Load font for countdown

        // Set initial player position at the lower part of the screen
        _playerPosition = new Vector2(
            (_graphicsDevice.Viewport.Width - _playerTexture.Width) / 2,
            _graphicsDevice.Viewport.Height - _playerTexture.Height - 50
        );
    }

    public override void Update(GameTime gameTime)
    {
        if (!_gameStarted)
        {
            // Decrease countdown timer
            _countdown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_countdown <= 0)
            {
                _gameStarted = true; // Start the game when countdown reaches 0
            }
            return; // Prevent game logic from running until countdown is over
        }

        // TODO: Player movement logic will be added here after the countdown
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_backgroundTexture,
            new Rectangle(0, 0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height),
            Color.White);

        if (!_gameStarted)
        {
            // Draw countdown text
            string countdownText = _countdown > 1 ? Math.Ceiling(_countdown).ToString() : "GO!";
            Vector2 textSize = _titleFont.MeasureString(countdownText);
            Vector2 textPosition = new Vector2(
                (_graphicsDevice.Viewport.Width - textSize.X * 10f) / 2, // Adjusted for scaling
                (_graphicsDevice.Viewport.Height - textSize.Y * 10f) / 2 // Adjusted for scaling
            );

            spriteBatch.DrawString(_titleFont, countdownText, textPosition, Color.White,
                0f, Vector2.Zero, 10f, SpriteEffects.None, 0f); // Scale text by 10
        }
        else
        {
            // Draw player only after the countdown is over
            spriteBatch.Draw(_playerTexture, _playerPosition, Color.White);
        }
    }
}
