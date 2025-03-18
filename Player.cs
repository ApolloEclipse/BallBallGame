// Player.cs
// Manages player movement, input handling, and collision detection.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Player : ICollidable
{
    private Texture2D _texture; // Player texture
    private Vector2 _position; // Player position
    private float _speed = 3f; // Movement speed
    private bool _movingUp = true; // Tracks movement direction

    private int minBoundaryY; // Upper boundary for movement
    private int maxBoundaryY; // Lower boundary for movement

    private KeyboardState _previousState; // Stores the previous keyboard state

    // Constructor initializes player position and movement boundaries
    public Player(Texture2D texture, Vector2 startPosition, int minY, int maxY)
    {
        _texture = texture;
        _position = startPosition;
        minBoundaryY = minY;
        maxBoundaryY = maxY;
    }

    // Updates player movement based on input
    public void Update(GameTime gameTime)
    {
        KeyboardState currentState = Keyboard.GetState();

        // Toggle movement direction when spacebar is pressed
        if (currentState.IsKeyDown(Keys.Space) && _previousState.IsKeyUp(Keys.Space))
        {
            _movingUp = !_movingUp;
        }

        // Move up or down depending on current direction
        _position.Y += _movingUp ? -_speed : _speed;

        // Ensure player stays within movement boundaries
        _position.Y = MathHelper.Clamp(_position.Y, minBoundaryY, maxBoundaryY);

        // Store the current keyboard state for the next frame
        _previousState = currentState;
    }

    // Draws the player on the screen
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, Color.White);
    }

    // Returns the player's collision box for collision detection
    public Rectangle GetBounds()
    {
        return new Rectangle((int)_position.X - 5, (int)_position.Y + 10, 70, 70); // Adjusted collider size
    }
}
