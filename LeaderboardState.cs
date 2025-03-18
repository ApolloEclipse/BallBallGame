// LeaderboardState.cs
// Manages the leaderboard screen, displaying the top scores and allowing navigation.

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class LeaderboardState : GameState
{
    private Texture2D _backgroundTexture; // Background image for the leaderboard screen
    private SpriteFont _font; // Font used for displaying leaderboard text
    private FileManager _fileManager; // Manages loading and saving scores
    private List<ScoreEntry> _scores; // Stores the top scores
    private string _filePath = "scores.json"; // File path for storing scores

    private Texture2D _restartButtonTexture;
    private Texture2D _menuButtonTexture;

    private Vector2 _restartButtonPosition;
    private Vector2 _menuButtonPosition;

    private Rectangle _restartButtonBounds;
    private Rectangle _menuButtonBounds;

    // Constructor initializes the leaderboard state
    public LeaderboardState(GameStateManager stateManager, GraphicsDevice graphicsDevice, ContentManager content)
        : base(stateManager, graphicsDevice, content)
    {
    }

    // Loads assets, initializes buttons, and loads leaderboard scores
    public override void Enter()
    {
        // Load textures and fonts
        _backgroundTexture = _content.Load<Texture2D>("UI/Images/LeaderBoardBG");
        _font = _content.Load<SpriteFont>("UI/Fonts/BebasNeueFont");

        _restartButtonTexture = _content.Load<Texture2D>("UI/Images/Bottuns/RestartButton");
        _menuButtonTexture = _content.Load<Texture2D>("UI/Images/Bottuns/ReturnToMainMenuButton");

        // Set button positions
        _restartButtonPosition = new Vector2(_graphicsDevice.Viewport.Width / 2 - _restartButtonTexture.Width / 2, 550);
        _menuButtonPosition = new Vector2(_graphicsDevice.Viewport.Width / 2 - _menuButtonTexture.Width / 2, 620);

        // Define button bounds for mouse click detection
        _restartButtonBounds = new Rectangle((int)_restartButtonPosition.X, (int)_restartButtonPosition.Y, _restartButtonTexture.Width, _restartButtonTexture.Height);
        _menuButtonBounds = new Rectangle((int)_menuButtonPosition.X, (int)_menuButtonPosition.Y, _menuButtonTexture.Width, _menuButtonTexture.Height);

        // Initialize FileManager to handle score loading
        _fileManager = new FileManager(_filePath);

        // Load the top scores from the file
        _scores = _fileManager.LoadScores();
    }

    // Handles user input for navigation
    public override void Update(GameTime gameTime)
    {
        MouseState mouseState = Mouse.GetState();

        // Check if the left mouse button is pressed on any button
        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            if (_restartButtonBounds.Contains(mouseState.Position))
            {
                _stateManager.SetState(new PlayingState(_stateManager, _graphicsDevice, _content)); // Restart game
            }
            else if (_menuButtonBounds.Contains(mouseState.Position))
            {
                _stateManager.SetState(new MainMenuState(_stateManager, _graphicsDevice, _content)); // Return to main menu
            }
        }
    }

    // Draws the leaderboard, including scores and navigation buttons
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // Draw background
        spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height), Color.White);

        // Display leaderboard title
        string title = "LEADERBOARD";
        Vector2 titleSize = _font.MeasureString(title);
        Vector2 titlePosition = new Vector2((_graphicsDevice.Viewport.Width - titleSize.X) / 2, 50);
        spriteBatch.DrawString(_font, title, titlePosition, Color.White);

        // Draw the top 10 scores
        int startY = 120; // Start position for scores
        for (int i = 0; i < Math.Min(_scores.Count, 10); i++)
        {
            string scoreText = $"{i + 1}. {_scores[i].PlayerName}: {_scores[i].Score}";
            Vector2 textSize = _font.MeasureString(scoreText);
            Vector2 textPosition = new Vector2((_graphicsDevice.Viewport.Width - textSize.X) / 2, startY + (i * 40));
            spriteBatch.DrawString(_font, scoreText, textPosition, Color.White);
        }

        // Draw buttons for restarting or returning to the menu
        spriteBatch.Draw(_restartButtonTexture, _restartButtonBounds, Color.White);
        spriteBatch.Draw(_menuButtonTexture, _menuButtonBounds, Color.White);

        // Draw button labels
        DrawButtonText(spriteBatch, "RESTART", _restartButtonPosition, _restartButtonTexture);
        DrawButtonText(spriteBatch, "MAIN MENU", _menuButtonPosition, _menuButtonTexture);
    }

    // Draws text centered on the buttons
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
