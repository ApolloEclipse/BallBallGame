using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// Represents the player character
public class Player
{
    private Texture2D _texture;
    private Vector2 _position;
    private float _speed = 300f; // Adjusted for smooth movement

    public Player(Texture2D texture, Vector2 startPosition)
    {
        _texture = texture;
        _position = startPosition;
    }

    public void Update(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (keyboardState.IsKeyDown(Keys.Up))
        {
            _position.Y -= _speed * deltaTime;
        }
        if (keyboardState.IsKeyDown(Keys.Down))
        {
            _position.Y += _speed * deltaTime;
        }

        // Ensure player stays within screen bounds
        _position.Y = MathHelper.Clamp(_position.Y, 0, 1080 - _texture.Height);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, Color.White);
    }
}
