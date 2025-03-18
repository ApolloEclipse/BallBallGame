// CollisionManager.cs
// Handles collision detection between the player and objects.

using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class CollisionManager
{
    private Player _player;
    private ScoreManager _scoreManager;
    private UIManager _uiManager; // ✅ UIManager to update life
    private List<IMovable> _gameObjects;

    public CollisionManager(Player player, ScoreManager scoreManager, UIManager uiManager, List<IMovable> gameObjects)
    {
        _player = player;
        _scoreManager = scoreManager;
        _uiManager = uiManager; // ✅ Store UIManager reference
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
                        _scoreManager.AddScore(scorable.GetScoreValue()); // ✅ Add score on Ball hit
                    }
                    else if (_gameObjects[i] is Debuff)
                    {
                        _uiManager.DecreaseLife(); // ✅ Reduce life on Debuff hit
                    }

                    _gameObjects.RemoveAt(i);
                }
            }
        }
    }
}
