// LevelManager.cs
// Manages game difficulty progression by gradually increasing the speed of moving objects over time.

using Microsoft.Xna.Framework;

public class LevelManager
{
    private float _scrollSpeed; // Current speed of moving objects
    private double _timeElapsed; // Tracks elapsed time to adjust difficulty

    // Initializes the level manager with a default scroll speed
    public LevelManager()
    {
        _scrollSpeed = 3f; // Starting speed for game objects
        _timeElapsed = 0;
    }

    // Updates the level difficulty over time by increasing speed at regular intervals
    public void Update(GameTime gameTime)
    {
        _timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

        // Every 10 seconds, increase the scroll speed slightly to ramp up difficulty
        if (_timeElapsed >= 10.0)
        {
            _scrollSpeed += 0.5f;
            _timeElapsed = 0; // Reset timer
        }
    }

    // Returns the current scroll speed of the game objects
    public float GetScrollSpeed()
    {
        return _scrollSpeed;
    }
}
