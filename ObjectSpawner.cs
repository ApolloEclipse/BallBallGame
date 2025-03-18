// ObjectSpawner.cs
// Manages the spawning of Balls (collectible objects) and Debuffs (obstacles) at set intervals.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class ObjectSpawner
{
    private List<IMovable> _gameObjects; // Stores the currently active objects
    private Texture2D _ballTexture; // Texture for Ball objects
    private Texture2D _debuffTexture; // Texture for Debuff objects
    private int _screenWidth; // Screen width for positioning new objects
    private int _minY, _maxY; // Y-axis boundaries for spawning objects
    private double _ballTimer; // Timer to control Ball spawning
    private double _debuffTimer; // Timer to control Debuff spawning
    private bool _initialSpawnDone; // Ensures initial objects spawn only once

    // Constructor initializes textures, screen size, and spawn boundaries
    public ObjectSpawner(Texture2D ballTexture, Texture2D debuffTexture, int screenWidth, int minY, int maxY)
    {
        _gameObjects = new List<IMovable>();
        _ballTexture = ballTexture;
        _debuffTexture = debuffTexture;
        _screenWidth = screenWidth;
        _minY = minY; // Minimum Y position (aligned with player movement range)
        _maxY = maxY;
        _ballTimer = 0;
        _debuffTimer = 0;
        _initialSpawnDone = false;
    }

    // Updates timers and spawns new objects at defined intervals
    public void Update(GameTime gameTime)
    {
        _ballTimer += gameTime.ElapsedGameTime.TotalSeconds;
        _debuffTimer += gameTime.ElapsedGameTime.TotalSeconds;

        // Spawn an initial Ball and Debuff at game start
        if (!_initialSpawnDone)
        {
            _gameObjects.Add(new Ball(_ballTexture, _screenWidth, _minY, _maxY));
            _gameObjects.Add(new Debuff(_debuffTexture, _screenWidth, _minY, _maxY));
            _initialSpawnDone = true;
        }

        // Spawn a Ball every 5 seconds
        if (_ballTimer >= 5.0)
        {
            _gameObjects.Add(new Ball(_ballTexture, _screenWidth, _minY, _maxY));
            _ballTimer = 0;
        }

        // Spawn a Debuff every 30 seconds
        if (_debuffTimer >= 30.0)
        {
            _gameObjects.Add(new Debuff(_debuffTexture, _screenWidth, _minY, _maxY));
            _debuffTimer = 0;
        }

        // Update all game objects
        foreach (var obj in _gameObjects)
        {
            obj.Update(gameTime);
        }
    }

    // Draws all spawned objects
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var obj in _gameObjects)
        {
            obj.Draw(spriteBatch);
        }
    }

    // Returns the list of currently active game objects for collision detection
    public List<IMovable> GetGameObjects()
    {
        return _gameObjects;
    }
}
