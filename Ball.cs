// Ball.cs
// Represents the collectible ball that moves towards the player and gives points.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Ball : IMovable, IScorable, ICollidable
{
    private Texture2D _texture;
    private Vector2 _position;
    private float _speed;
    private int _screenWidth;
    private int _minY, _maxY;
    private const int _scoreValue = 10; // Each ball gives 10 points

    public Ball(Texture2D texture, int screenWidth, int minY, int maxY)
    {
        _texture = texture;
        _screenWidth = screenWidth;
        _minY = minY;
        _maxY = maxY;
        ResetPosition();
        _speed = 3f; // Fixed speed
    }

    public void Update(GameTime gameTime)
    {
        _position.X -= _speed; // Move left

        // Reset position if off screen
        if (_position.X < -_texture.Width)
        {
            ResetPosition();
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, Color.White);
    }

    private void ResetPosition()
    {
        Random random = new Random();
        _position = new Vector2(_screenWidth, random.Next(_minY, _maxY));
    }

    public int GetScoreValue()
    {
        return _scoreValue;
    }

    public Rectangle GetBounds()
    {
        return new Rectangle((int)_position.X, (int)_position.Y, 45, 45); // Correct Ball size
    }
}
