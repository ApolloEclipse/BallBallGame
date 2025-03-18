// TimerDisplay.cs
// Manages a timer display, tracking elapsed time and rendering it on the screen.

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class TimerDisplay
{
    private SpriteFont _font; // Font used to display the timer
    private Vector2 _position; // Position of the timer on the screen
    private TimeSpan _elapsedTime; // Tracks elapsed time since the timer started
    private bool _isRunning; // Determines whether the timer is active

    // Constructor initializes the timer with a font and position
    public TimerDisplay(ContentManager content, Vector2 position)
    {
        _font = content.Load<SpriteFont>("UI/Fonts/BebasNeueFont");
        _position = position;
        _elapsedTime = TimeSpan.Zero;
        _isRunning = false;
    }

    // Starts the timer and resets elapsed time
    public void Start()
    {
        _isRunning = true;
        _elapsedTime = TimeSpan.Zero;
    }

    // Stops the timer
    public void Stop()
    {
        _isRunning = false;
    }

    // Updates the timer while it is running
    public void Update(GameTime gameTime)
    {
        if (_isRunning)
        {
            _elapsedTime += gameTime.ElapsedGameTime;
        }
    }

    // Draws the formatted timer on the screen
    public void Draw(SpriteBatch spriteBatch)
    {
        string formattedTime = $"{_elapsedTime.Minutes:D2}:{_elapsedTime.Seconds:D2}:{_elapsedTime.Milliseconds / 10:D2}";
        spriteBatch.DrawString(_font, formattedTime, _position, Color.White);
    }
}
