// LeaderboardState.cs
// Manages the leaderboard screen, displaying the top scores.

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class LeaderboardState : GameState
{
    private Texture2D _backgroundTexture;
    private SpriteFont _font;
    private FileManager _fileManager;
    private List<ScoreEntry> _scores;
    private string _filePath = "scores.json"; // Relative path to the scores file

    private Texture2D _restartButtonTexture;
    private Texture2D _menuButtonTexture;

    private Vector2 _restartButtonPosition;
    private Vector2 _menuButtonPosition;

    private Rectangle _restartButtonBounds;
    private Rectangle _menuButtonBounds;

    public LeaderboardState(GameStateManager stateManager, GraphicsDevice graphicsDevice, ContentManager content)
        : base(stateManager, graphicsDevice, content)
    {
    }

    public override void Enter()
    {
        // Load assets
        _backgroundTexture = _content.Load<Texture2D>("UI/Images/LeaderBoardBG");
        _font = _content.Load<SpriteFont>("UI/Fonts/BebasNeueFont");

        _restartButtonTexture = _content.Load<Texture2D>("UI/Images/Bottuns/RestartButton");
        _menuButtonTexture = _content.Load<Texture2D>("UI/Images/Bottuns/ReturnToMainMenuButton");

        // Lowering buttons and increasing spacing
        _restartButtonPosition = new Vector2(_graphicsDevice.Viewport.Width / 2 - _restartButtonTexture.Width / 2, 550);
        _menuButtonPosition = new Vector2(_graphicsDevice.Viewport.Width / 2 - _menuButtonTexture.Width / 2, 620);

        _restartButtonBounds = new Rectangle((int)_restartButtonPosition.X, (int)_restartButtonPosition.Y, _restartButtonTexture.Width, _restartButtonTexture.Height);
        _menuButtonBounds = new Rectangle((int)_menuButtonPosition.X, (int)_menuButtonPosition.Y, _menuButtonTexture.Width, _menuButtonTexture.Height);

        // Initialize FileManager with file path
        _fileManager = new FileManager(_filePath);

        // Load scores
        _scores = _fileManager.LoadScores();
    }

    public override void Update(GameTime gameTime)
    {
        MouseState mouseState = Mouse.GetState();

        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            if (_restartButtonBounds.Contains(mouseState.Position))
            {
                _stateManager.SetState(new PlayingState(_stateManager, _graphicsDevice, _content));
            }
            else if (_menuButtonBounds.Contains(mouseState.Position))
            {
                _stateManager.SetState(new MainMenuState(_stateManager, _graphicsDevice, _content));
            }
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // Draw background
        spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height), Color.White);

        // Display leaderboard title
        string title = "LEADERBOARD";
        Vector2 titleSize = _font.MeasureString(title);
        Vector2 titlePosition = new Vector2((_graphicsDevice.Viewport.Width - titleSize.X) / 2, 50);
        spriteBatch.DrawString(_font, title, titlePosition, Color.White);

        // Draw top 10 scores
        int startY = 120;
        for (int i = 0; i < Math.Min(_scores.Count, 10); i++)
        {
            string scoreText = $"{i + 1}. {_scores[i].PlayerName}: {_scores[i].Score}";
            Vector2 textSize = _font.MeasureString(scoreText);
            Vector2 textPosition = new Vector2((_graphicsDevice.Viewport.Width - textSize.X) / 2, startY + (i * 40));
            spriteBatch.DrawString(_font, scoreText, textPosition, Color.White);
        }

        // Draw buttons
        spriteBatch.Draw(_restartButtonTexture, _restartButtonBounds, Color.White);
        spriteBatch.Draw(_menuButtonTexture, _menuButtonBounds, Color.White);

        // Draw button text
        DrawButtonText(spriteBatch, "RESTART", _restartButtonPosition, _restartButtonTexture);
        DrawButtonText(spriteBatch, "MAIN MENU", _menuButtonPosition, _menuButtonTexture);
    }

    private void DrawButtonText(SpriteBatch spriteBatch, string text, Vector2 buttonPosition, Texture2D buttonTexture)
    {
        Vector2 textSize = _font.MeasureString(text);
        Vector2 textPosition = new Vector2(
            buttonPosition.X + (buttonTexture.Width - textSize.X) / 2,
            buttonPosition.Y + (buttonTexture.Height - textSize.Y) / 2
        );
        spriteBatch.DrawString(_font, text, textPosition, Color.Black);
    }
}
