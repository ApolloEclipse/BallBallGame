// Ball.cs
// Represents the collectible ball that moves towards the player.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Ball : IMovable
{
    private Texture2D _texture;  // Ball texture
    private Vector2 _position;   // Ball's position
    private float _speed;        // Movement speed
    private int _screenWidth;    // Game screen width
    private int _minY, _maxY;    // Movement boundaries

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
        _position = new Vector2(_screenWidth, random.Next(_minY, _maxY)); // Spawn within player's movement range
    }
}
