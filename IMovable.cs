// IMovable.cs
// Defines the interface for all moving objects in the game.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IMovable
{
    void Update(GameTime gameTime);  // Updates the object's position.
    void Draw(SpriteBatch spriteBatch); // Renders the object on the screen.
}
