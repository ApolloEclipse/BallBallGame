// ICollidable.cs
// Interface for objects that can be collided with.

using Microsoft.Xna.Framework;

public interface ICollidable
{
    Rectangle GetBounds();  // Returns the object's collision box.
}
