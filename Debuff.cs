// Debuff.cs
// Represents a moving obstacle that can collide with the player and cause negative effects.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Debuff : IMovable, ICollidable
{
    private Texture2D _texture; // The texture representing the debuff
    private Vector2 _position; // Current position of the debuff
    private float _speed; // Movement speed of the debuff
    private int _screenWidth; // Screen width for resetting position
    private int _minY, _maxY; // The vertical range for spawning the debuff

    // Constructor initializes the debuff with texture, screen width, and spawn boundaries
    public Debuff(Texture2D texture, int screenWidth, int minY, int maxY)
    {
        _texture = texture;
        _screenWidth = screenWidth;
        _minY = minY;
        _maxY = maxY;
        ResetPosition(); // Set the initial position
        _speed = 3f; // Set a fixed speed for movement
    }

    // Updates the debuff’s position, moving it from right to left across the screen
    public void Update(GameTime gameTime)
    {
        _position.X -= _speed; // Move left

        // If the debuff moves off-screen, reset its position to the right side
        if (_position.X < -_texture.Width)
        {
            ResetPosition();
        }
    }

    // Draws the debuff on the screen at its current position
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, Color.White);
    }

    // Resets the debuff’s position to the right side at a random height within the allowed range
    private void ResetPosition()
    {
        Random random = new Random();
        _position = new Vector2(_screenWidth, random.Next(_minY, _maxY));
    }

    // Returns the bounding box of the debuff for collision detection
    public Rectangle GetBounds()
    {
        return new Rectangle((int)_position.X, (int)_position.Y, 100, 100);
    }
}
