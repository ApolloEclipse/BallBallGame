using Microsoft.Xna.Framework;

public abstract class GameState
{
    protected GameStateManager _stateManager; // Reference to the state manager, This allows each state to change states when needed

    // Constructor - receives the state manager as a parameter , This allows the state to change to another state if needed
    public GameState(GameStateManager stateManager)
    {
        _stateManager = stateManager;
    }

    public abstract void Enter();  // Called when this state becomes active
    public abstract void Exit();   // Called when switching away from this state
    public abstract void Update(GameTime gameTime); // Handles game logic
    public abstract void Draw(GameTime gameTime);   // Handles rendering
}
