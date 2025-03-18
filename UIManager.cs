// UIManager.cs
// Manages UI updates and listens for event-based changes.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class UIManager
{
    private SpriteFont _font;
    private ScoreManager _scoreManager;
    private int _lives = 3;
    private Vector2 _scorePosition;
    private Vector2 _lifePosition;
    private string _scoreText;
    private string _lifeText;

    public UIManager(SpriteFont font, ScoreManager scoreManager)
    {
        _font = font;
        _scoreManager = scoreManager;
        _scorePosition = new Vector2(400, 20);
        _lifePosition = new Vector2(400, 50);
        _scoreText = "Score: 0";
        _lifeText = "Life: 3";

        // ✅ Ensure event listeners are not duplicated
        Unsubscribe();
        EventManager.Instance.OnScoreUpdated += UpdateScoreDisplay;
        EventManager.Instance.OnLifeLost += DecreaseLife;
    }

    private void UpdateScoreDisplay(int newScore)
    {
        _scoreText = $"Score: {newScore}";
    }

    public void DecreaseLife()
    {
        if (_lives > 0)
        {
            _lives--;
            _lifeText = $"Life: {_lives}";
        }
    }

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
        string scoreText = "Score: " + _scoreManager.GetScore();
        string lifeText = "Life: " + _lives;

        Vector2 scoreSize = _font.MeasureString(scoreText);
        Vector2 lifeSize = _font.MeasureString(lifeText);

        // UI Box Position (Centered)
        Vector2 uiBoxPosition = new Vector2(
            (graphicsDevice.Viewport.Width - 250) / 2, // Centered Horizontally
            20 // Top margin
        );
        Vector2 uiBoxSize = new Vector2(250, 80);

        // Centered Score Text Position
        Vector2 scorePosition = new Vector2(
            uiBoxPosition.X + (uiBoxSize.X - scoreSize.X) / 2, // Center X
            uiBoxPosition.Y + 10 // Some top margin
        );

        // Centered Life Text Below Score
        Vector2 lifePosition = new Vector2(
            uiBoxPosition.X + (uiBoxSize.X - lifeSize.X) / 2, // Center X
            scorePosition.Y + scoreSize.Y + 5 // Below Score
        );

        // ✅ Draw the text in the center of the UI Box
        spriteBatch.DrawString(_font, scoreText, scorePosition, Color.White);
        spriteBatch.DrawString(_font, lifeText, lifePosition, Color.White);
    }

    // ✅ Unsubscribe method to prevent duplicate event listeners
    public void Unsubscribe()
    {
        EventManager.Instance.OnScoreUpdated -= UpdateScoreDisplay;
        EventManager.Instance.OnLifeLost -= DecreaseLife;
    }
}
