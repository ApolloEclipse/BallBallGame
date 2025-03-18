using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class TimerDisplay
{
    private SpriteFont _font;
    private Vector2 _position;
    private TimeSpan _elapsedTime;
    private bool _isRunning;

    public TimerDisplay(ContentManager content, Vector2 position)
    {
        _font = content.Load<SpriteFont>("UI/Fonts/BebasNeueFont");
        _position = position;
        _elapsedTime = TimeSpan.Zero;
        _isRunning = false;
    }

    public void Start()
    {
        _isRunning = true;
        _elapsedTime = TimeSpan.Zero;
    }

    public void Stop()
    {
        _isRunning = false;
    }

    public void Update(GameTime gameTime)
    {
        if (_isRunning)
        {
            _elapsedTime += gameTime.ElapsedGameTime;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        string formattedTime = $"{_elapsedTime.Minutes:D2}:{_elapsedTime.Seconds:D2}:{_elapsedTime.Milliseconds / 10:D2}";
        spriteBatch.DrawString(_font, formattedTime, _position, Color.White);
    }
}
