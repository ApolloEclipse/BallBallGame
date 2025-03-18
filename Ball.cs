// Ball.cs
// Handles the collectible ball behavior, including movement, scoring, and collision detection.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Ball : IMovable, IScorable, ICollidable
{
    private Texture2D _texture; // The texture representing the ball
    private Vector2 _position; // Current position of the ball
    private float _speed; // Movement speed of the ball
    private int _screenWidth; // Screen width to reset position when needed
    private int _minY, _maxY; // The vertical range for spawning the ball
    private const int _scoreValue = 10; // Score value awarded when collected

    // Constructor initializes the ball with texture, screen width, and spawn boundaries
    public Ball(Texture2D texture, int screenWidth, int minY, int maxY)
    {
        _texture = texture;
        _screenWidth = screenWidth;
        _minY = minY;
        _maxY = maxY;
        ResetPosition(); // Set the initial position
        _speed = 3f; // Set a fixed speed for movement
    }

    // Updates the ball's position, moving it from right to left across the screen
    public void Update(GameTime gameTime)
    {
        _position.X -= _speed; // Move left

        // If the ball moves off-screen, reset its position to the right side
        if (_position.X < -_texture.Width)
        {
            ResetPosition();
        }
    }

    // Draws the ball on the screen at its current position
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, Color.White);
    }

    // Resets the ball's position to the right side at a random height within the allowed range
    private void ResetPosition()
    {
        Random random = new Random();
        _position = new Vector2(_screenWidth, random.Next(_minY, _maxY));
    }

    // Returns the score value of the ball when collected
    public int GetScoreValue()
    {
        return _scoreValue;
    }

    // Returns the bounding box of the ball for collision detection
    public Rectangle GetBounds()
    {
        return new Rectangle((int)_position.X, (int)_position.Y, 45, 45);
    }
}
