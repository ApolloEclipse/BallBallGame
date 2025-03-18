// ICollidable.cs
// Defines an interface for objects that support collision detection.

using Microsoft.Xna.Framework;

public interface ICollidable
{
    // Returns the bounding box of the object, used for collision detection.
    Rectangle GetBounds();
}
