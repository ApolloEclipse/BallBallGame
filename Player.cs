// Player.cs
// Handles player movement and collision detection.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Player : ICollidable
{
    private Texture2D _texture;
    private Vector2 _position;
    private float _speed = 3f;
    private bool _movingUp = true;

    private int minBoundaryY;
    private int maxBoundaryY;

    private KeyboardState _previousState;

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

        if (currentState.IsKeyDown(Keys.Space) && _previousState.IsKeyUp(Keys.Space))
        {
            _movingUp = !_movingUp;
        }

        _position.Y += _movingUp ? -_speed : _speed;

        _position.Y = MathHelper.Clamp(_position.Y, minBoundaryY, maxBoundaryY);

        _previousState = currentState;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, Color.White);
    }

    public Rectangle GetBounds()
    {
        return new Rectangle((int)_position.X - 5, (int)_position.Y + 10, 70, 70); // Player collider size
    }
}
