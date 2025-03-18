// CollisionManager.cs
// Manages collision detection between the player and game objects,
// handling score updates and triggering life loss events when needed.

using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class CollisionManager
{
    private Player _player; // Reference to the player for collision checking
    private ScoreManager _scoreManager; // Manages player score
    private UIManager _uiManager; // Handles UI updates
    private PlayingState _playingState; // Reference to the game state managing active gameplay
    private List<IMovable> _gameObjects; // List of active game objects that can move and collide

    // Constructor initializes all necessary components for collision detection
    public CollisionManager(Player player, ScoreManager scoreManager, UIManager uiManager, PlayingState playingState, List<IMovable> gameObjects)
    {
        _player = player;
        _scoreManager = scoreManager;
        _uiManager = uiManager;
        _playingState = playingState;
        _gameObjects = gameObjects;
    }

    // Checks for collisions between the player and game objects, updating score or triggering life loss
    public void Update()
    {
        Rectangle playerBounds = _player.GetBounds(); // Get the player's collision bounds

        // Iterate through game objects in reverse to allow safe removal when needed
        for (int i = _gameObjects.Count - 1; i >= 0; i--)
        {
            if (_gameObjects[i] is ICollidable collidable)
            {
                // Check if the player collides with the current game object
                if (playerBounds.Intersects(collidable.GetBounds()))
                {
                    // If the object is scorable (like a Ball), increase the player's score
                    if (_gameObjects[i] is IScorable scorable)
                    {
                        _scoreManager.IncreaseScore(scorable.GetScoreValue());
                    }
                    // If the object is a Debuff, trigger the life lost event
                    else if (_gameObjects[i] is Debuff)
                    {
                        if (_player != null) // Ensure the player reference is valid before triggering the event
                        {
                            EventManager.Instance.TriggerLifeLost();
                        }
                    }

                    // Remove the collided object from the game to prevent repeated interactions
                    _gameObjects.RemoveAt(i);
                    break; // Exit the loop to ensure only one object is processed per frame
                }
            }
        }
    }
}
