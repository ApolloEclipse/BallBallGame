using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Player
{
    private Texture2D _texture; // Player texture
    private Vector2 _position; // Player position
    private float _speed = 3f; // Player movement speed
    private bool _movingUp = true; // Controls movement direction

    private int minBoundaryY; // Minimum Y boundary
    private int maxBoundaryY; // Maximum Y boundary

    private KeyboardState _previousState; // Tracks previous key state

    public Player(Texture2D texture, Vector2 startPosition, int minY, int maxY)
    {
        _texture = texture;
        _position = startPosition;
        minBoundaryY = minY;
        maxBoundaryY = maxY;
    }

    public void Update(GameTime gameTime)
    {
        KeyboardState currentState = Keyboard.GetState();

        // Toggle direction only on the first key press (prevents multiple flips)
        if (currentState.IsKeyDown(Keys.Space) && _previousState.IsKeyUp(Keys.Space))
        {
            _movingUp = !_movingUp;
        }

        // Move player based on direction
        _position.Y += _movingUp ? -_speed : _speed;

        // Keep player within boundaries
        _position.Y = MathHelper.Clamp(_position.Y, minBoundaryY, maxBoundaryY);

        _previousState = currentState; // Store the current state for the next frame
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, Color.White);
    }
}