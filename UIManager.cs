// UIManager.cs
// Manages UI elements, updates score and life count, and listens for event-based changes.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class UIManager
{
    private SpriteFont _font; // Font used for displaying UI elements
    private ScoreManager _scoreManager; // Reference to the score manager
    private int _lives = 3; // Player's remaining lives
    private Vector2 _scorePosition; // Position of the score display
    private Vector2 _lifePosition; // Position of the life display
    private string _scoreText; // Displayed score text
    private string _lifeText; // Displayed life text

    // Constructor initializes UI properties and subscribes to events
    public UIManager(SpriteFont font, ScoreManager scoreManager)
    {
        _font = font;
        _scoreManager = scoreManager;
        _scorePosition = new Vector2(400, 20);
        _lifePosition = new Vector2(400, 50);
        _scoreText = "Score: 0";
        _lifeText = "Life: 3";

        // Ensure event listeners are not duplicated
        Unsubscribe();
        EventManager.Instance.OnScoreUpdated += UpdateScoreDisplay;
        EventManager.Instance.OnLifeLost += DecreaseLife;
    }

    // Updates the displayed score when the score is changed
    private void UpdateScoreDisplay(int newScore)
    {
        _scoreText = $"Score: {newScore}";
    }

    // Decreases the life count and updates the display when the player loses a life
    public void DecreaseLife()
    {
        if (_lives > 0)
        {
            _lives--;
            _lifeText = $"Life: {_lives}";
        }
    }

    // Draws the UI elements, including score and life count, at the top center of the screen
    public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
        string scoreText = "Score: " + _scoreManager.GetScore();
        string lifeText = "Life: " + _lives;

        Vector2 scoreSize = _font.MeasureString(scoreText);
        Vector2 lifeSize = _font.MeasureString(lifeText);

        // UI Box Position (Centered at the top of the screen)
        Vector2 uiBoxPosition = new Vector2(
            (graphicsDevice.Viewport.Width - 250) / 2, // Centered Horizontally
            20 // Top margin
        );
        Vector2 uiBoxSize = new Vector2(250, 80);

        // Position score text inside the UI box
        Vector2 scorePosition = new Vector2(
            uiBoxPosition.X + (uiBoxSize.X - scoreSize.X) / 2, // Centered horizontally
            uiBoxPosition.Y + 10 // Some top margin
        );

        // Position life text below the score text
        Vector2 lifePosition = new Vector2(
            uiBoxPosition.X + (uiBoxSize.X - lifeSize.X) / 2, // Centered horizontally
            scorePosition.Y + scoreSize.Y + 5 // Below score text
        );

        // Draw score and life text on screen
        spriteBatch.DrawString(_font, scoreText, scorePosition, Color.White);
        spriteBatch.DrawString(_font, lifeText, lifePosition, Color.White);
    }

    // Unsubscribes event listeners to prevent duplicate event subscriptions
    public void Unsubscribe()
    {
        EventManager.Instance.OnScoreUpdated -= UpdateScoreDisplay;
        EventManager.Instance.OnLifeLost -= DecreaseLife;
    }
}
