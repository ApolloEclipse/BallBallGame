// UIManager.cs
// Manages the in-game UI, displaying score and lives on the screen.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class UIManager
{
    private SpriteFont _font;
    private ScoreManager _scoreManager;
    private int _lives; // ✅ Life counter

    public UIManager(SpriteFont font, ScoreManager scoreManager)
    {
        _font = font;
        _scoreManager = scoreManager;
        _lives = 3; // ✅ Start with 3 lives
    }

    // ✅ Method to decrease life when player hits a Debuff
    public void DecreaseLife()
    {
        if (_lives > 0)
            _lives--;
    }

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
        // ✅ Score Position: Centered in the Top UI Bar
        string scoreText = "Score: " + _scoreManager.GetScore();
        Vector2 scoreSize = _font.MeasureString(scoreText);
        Vector2 scorePosition = new Vector2(
            (graphicsDevice.Viewport.Width - scoreSize.X) / 2, // Centered X
            20 // Top margin
        );

        // ✅ Life Counter Position: Below the Score
        string lifeText = "Life: " + _lives;
        Vector2 lifeSize = _font.MeasureString(lifeText);
        Vector2 lifePosition = new Vector2(
            (graphicsDevice.Viewport.Width - lifeSize.X) / 2, // Centered X
            scorePosition.Y + scoreSize.Y + 10 // Below Score
        );

        // ✅ Draw Score & Life Counter
        spriteBatch.DrawString(_font, scoreText, scorePosition, Color.White);
        spriteBatch.DrawString(_font, lifeText, lifePosition, Color.White);
    }
}
