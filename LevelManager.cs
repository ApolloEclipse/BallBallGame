// LevelManager.cs
// Adjusts game difficulty over time by increasing speed.

using Microsoft.Xna.Framework;

public class LevelManager
{
    private float _scrollSpeed; // Current object speed
    private double _timeElapsed; // Time tracker

    public LevelManager()
    {
        _scrollSpeed = 3f; // Starting speed
        _timeElapsed = 0;
    }

    public void Update(GameTime gameTime)
    {
        _timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

        // Every 10 seconds, increase difficulty
        if (_timeElapsed >= 10.0)
        {
            _scrollSpeed += 0.5f; // Increase speed slightly
            _timeElapsed = 0;
        }
    }

    public float GetScrollSpeed()
    {
        return _scrollSpeed;
    }
}
