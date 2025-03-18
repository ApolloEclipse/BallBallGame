// IMovable.cs
// Defines an interface for all objects that move in the game.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IMovable
{
    // Updates the object's position and movement logic.
    void Update(GameTime gameTime);

    // Renders the object on the screen.
    void Draw(SpriteBatch spriteBatch);
}
