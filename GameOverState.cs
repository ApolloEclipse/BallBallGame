// GameOverState.cs
// Manages the Game Over screen, allowing the player to enter their name, save their score,
// and navigate to different game states.

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class GameOverState : GameState
{
    private Texture2D _backgroundTexture; // Background image for the Game Over screen
    private SpriteFont _font; // Font for displaying text
    private int _finalScore; // Stores the player's final score
    private string _playerName; // Stores the player's entered name
    private FileManager _fileManager; // Handles score saving/loading
    private string _filePath = "scores.json"; // File path for saving scores

    private Texture2D _restartButtonTexture;
    private Texture2D _menuButtonTexture;
    private Texture2D _leaderboardButtonTexture;

    private Vector2 _restartButtonPosition;
    private Vector2 _menuButtonPosition;
    private Vector2 _leaderboardButtonPosition;

    private Rectangle _restartButtonBounds;
    private Rectangle _menuButtonBounds;
    private Rectangle _leaderboardButtonBounds;

    private bool _isEnteringName; // True if the player is entering their name
    private KeyboardState _previousKeyboardState; // Stores previous keyboard state to prevent duplicate input

    // Constructor initializes the game over state with the final player score
    public GameOverState(GameStateManager stateManager, GraphicsDevice graphicsDevice, ContentManager content, int finalScore)
        : base(stateManager, graphicsDevice, content)
    {
        _finalScore = finalScore;
        _playerName = "";
        _isEnteringName = true;
    }

    // Loads assets and initializes button positions when entering the game over state
    public override void Enter()
    {
        _backgroundTexture = _content.Load<Texture2D>("UI/Images/GameOverBG");
        _font = _content.Load<SpriteFont>("UI/Fonts/BebasNeueFont");

        _restartButtonTexture = _content.Load<Texture2D>("UI/Images/Bottuns/RestartButton");
        _menuButtonTexture = _content.Load<Texture2D>("UI/Images/Bottuns/ReturnToMainMenuButton");
        _leaderboardButtonTexture = _content.Load<Texture2D>("UI/Images/Bottuns/LeaderboardButton");

        _restartButtonPosition = new Vector2(_graphicsDevice.Viewport.Width / 2 - _restartButtonTexture.Width / 2, 500);
        _menuButtonPosition = new Vector2(_graphicsDevice.Viewport.Width / 2 - _menuButtonTexture.Width / 2, 580);
        _leaderboardButtonPosition = new Vector2(_graphicsDevice.Viewport.Width / 2 - _leaderboardButtonTexture.Width / 2, 660);

        _restartButtonBounds = new Rectangle((int)_restartButtonPosition.X, (int)_restartButtonPosition.Y, _restartButtonTexture.Width, _restartButtonTexture.Height);
        _menuButtonBounds = new Rectangle((int)_menuButtonPosition.X, (int)_menuButtonPosition.Y, _menuButtonTexture.Width, _menuButtonTexture.Height);
        _leaderboardButtonBounds = new Rectangle((int)_leaderboardButtonPosition.X, (int)_leaderboardButtonPosition.Y, _leaderboardButtonTexture.Width, _leaderboardButtonTexture.Height);

        _fileManager = new FileManager(_filePath);
        _previousKeyboardState = Keyboard.GetState();
    }

    // Updates player input for name entry and handles button clicks for navigation
    public override void Update(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();
        MouseState mouseState = Mouse.GetState();

        if (_isEnteringName)
        {
            foreach (Keys key in keyboardState.GetPressedKeys())
            {
                if (_previousKeyboardState.IsKeyUp(key)) // Prevents multiple inputs per keypress
                {
                    if (key == Keys.Enter)
                    {
                        _isEnteringName = false;
                        SaveScore();
                    }
                    else if (key == Keys.Back && _playerName.Length > 0)
                    {
                        _playerName = _playerName.Substring(0, _playerName.Length - 1);
                    }
                    else if (_playerName.Length < 10 && key != Keys.Back && key != Keys.Enter)
                    {
                        _playerName += key.ToString();
                    }
                }
            }
            _previousKeyboardState = keyboardState;
        }
        else
        {
            // Handle button clicks for restarting, returning to the menu, or viewing the leaderboard
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
                else if (_leaderboardButtonBounds.Contains(mouseState.Position))
                {
                    _stateManager.SetState(new LeaderboardState(_stateManager, _graphicsDevice, _content));
                }
            }
        }
    }

    // Saves the player's name and score to the leaderboard file
    private void SaveScore()
    {
        List<ScoreEntry> scores = _fileManager.LoadScores();
        scores.Add(new ScoreEntry(_playerName, _finalScore));

        // Sort scores in descending order
        scores.Sort((a, b) => b.Score.CompareTo(a.Score));

        // Keep only the top 10 scores
        if (scores.Count > 10)
        {
            scores.RemoveAt(10);
        }

        _fileManager.SaveScores(scores);
    }

    // Draws the game over screen, including score, name entry, and buttons
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height), Color.White);

        // Display the final score
        string scoreText = "FINAL SCORE: " + _finalScore;
        Vector2 scoreSize = _font.MeasureString(scoreText);
        Vector2 scorePosition = new Vector2((_graphicsDevice.Viewport.Width - scoreSize.X) / 2, 100);
        spriteBatch.DrawString(_font, scoreText, scorePosition, Color.White);

        if (_isEnteringName)
        {
            // Display name entry prompt
            string namePrompt = "ENTER NAME: " + _playerName;
            Vector2 nameSize = _font.MeasureString(namePrompt);
            Vector2 namePosition = new Vector2((_graphicsDevice.Viewport.Width - nameSize.X) / 2, 200);
            spriteBatch.DrawString(_font, namePrompt, namePosition, Color.White);
        }
        else
        {
            // Draw buttons for Restart, Main Menu, and Leaderboard navigation
            spriteBatch.Draw(_restartButtonTexture, _restartButtonBounds, Color.White);
            spriteBatch.Draw(_menuButtonTexture, _menuButtonBounds, Color.White);
            spriteBatch.Draw(_leaderboardButtonTexture, _leaderboardButtonBounds, Color.White);

            DrawButtonText(spriteBatch, "RESTART", _restartButtonPosition);
            DrawButtonText(spriteBatch, "MAIN MENU", _menuButtonPosition);
            DrawButtonText(spriteBatch, "LEADERBOARD", _leaderboardButtonPosition);
        }
    }

    // Draws text on buttons by centering it within the button area
    private void DrawButtonText(SpriteBatch spriteBatch, string text, Vector2 buttonPosition)
    {
        Vector2 textSize = _font.MeasureString(text);
        Vector2 textPosition = new Vector2(
            buttonPosition.X + (_restartButtonTexture.Width - textSize.X) / 2,
            buttonPosition.Y + (_restartButtonTexture.Height - textSize.Y) / 2
        );
        spriteBatch.DrawString(_font, text, textPosition, Color.Black);
    }
}
