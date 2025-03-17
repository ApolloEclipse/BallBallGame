// ObjectSpawner.cs
// Handles the automatic spawning of Balls and Debuffs.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class ObjectSpawner
{
    private List<IMovable> _gameObjects; // List of active objects
    private Texture2D _ballTexture;  // Ball texture
    private Texture2D _debuffTexture; // Debuff texture
    private int _screenWidth;
    private int _minY, _maxY; // Movement boundaries
    private double _ballTimer;
    private double _debuffTimer;
    private bool _initialSpawnDone;

    public ObjectSpawner(Texture2D ballTexture, Texture2D debuffTexture, int screenWidth, int minY, int maxY)
    {
        _gameObjects = new List<IMovable>();
        _ballTexture = ballTexture;
        _debuffTexture = debuffTexture;
        _screenWidth = screenWidth;
        _minY = minY; // Aligns with player movement
        _maxY = maxY;
        _ballTimer = 0;
        _debuffTimer = 0;
        _initialSpawnDone = false;
    }

    public void Update(GameTime gameTime)
    {
        _ballTimer += gameTime.ElapsedGameTime.TotalSeconds;
        _debuffTimer += gameTime.ElapsedGameTime.TotalSeconds;

        if (!_initialSpawnDone) // Spawn an initial Ball & Debuff immediately at game start
        {
            _gameObjects.Add(new Ball(_ballTexture, _screenWidth, _minY, _maxY));
            _gameObjects.Add(new Debuff(_debuffTexture, _screenWidth, _minY, _maxY));
            _initialSpawnDone = true;
        }

        if (_ballTimer >= 5.0)
        {
            _gameObjects.Add(new Ball(_ballTexture, _screenWidth, _minY, _maxY));
            _ballTimer = 0;
        }

        if (_debuffTimer >= 30.0)
        {
            _gameObjects.Add(new Debuff(_debuffTexture, _screenWidth, _minY, _maxY));
            _debuffTimer = 0;
        }

        foreach (var obj in _gameObjects)
        {
            obj.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var obj in _gameObjects)
        {
            obj.Draw(spriteBatch);
        }
    }
}
