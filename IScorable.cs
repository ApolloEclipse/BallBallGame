// IScorable.cs
// Defines an interface for objects that provide score when collected.

public interface IScorable
{
    // Returns the amount of score awarded when the object is collected.
    int GetScoreValue();
}
