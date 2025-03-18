// CollisionManager.cs
// Handles collision detection and event triggering.

using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class CollisionManager
{
    private Player _player;
    private ScoreManager _scoreManager;
    private UIManager _uiManager;
    private PlayingState _playingState;
    private List<IMovable> _gameObjects;

    public CollisionManager(Player player, ScoreManager scoreManager, UIManager uiManager, PlayingState playingState, List<IMovable> gameObjects)
    {
        _player = player;
        _scoreManager = scoreManager;
        _uiManager = uiManager;
        _playingState = playingState;
        _gameObjects = gameObjects;
    }

    public void Update()
    {
        Rectangle playerBounds = _player.GetBounds();

        for (int i = _gameObjects.Count - 1; i >= 0; i--)
        {
            if (_gameObjects[i] is ICollidable collidable)
            {
                if (playerBounds.Intersects(collidable.GetBounds()))
                {
                    if (_gameObjects[i] is IScorable scorable)
                    {
                        _scoreManager.IncreaseScore(scorable.GetScoreValue()); // ✅ Add score for Balls
                    }
                    else if (_gameObjects[i] is Debuff)
                    {
                        System.Diagnostics.Debug.WriteLine("Collision with Debuff Detected!");

                        if (_player != null) // ✅ Ensure the player exists
                        {
                            EventManager.Instance.TriggerLifeLost(); // ✅ Call the life lost event
                        }
                    }

                    _gameObjects.RemoveAt(i); // ✅ Remove the object only once
                    break; // ✅ Ensure only one object is processed per frame
                }
            }
        }
    }

}
