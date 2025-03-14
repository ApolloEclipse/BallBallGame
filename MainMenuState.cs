using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// Represents the main menu state in the game
public class MainMenuState : GameState
{
    private Texture2D _backgroundTexture;
    private SpriteFont _titleFont;

    public MainMenuState(GameStateManager stateManager, GraphicsDevice graphicsDevice, ContentManager content)
        : base(stateManager, graphicsDevice, content)
    {
    }

    public override void Enter()
    {
        // Load background texture
        _backgroundTexture = _content.Load<Texture2D>("UI/Images/MainMenuBG");

        // Load font for title text
        _titleFont = _content.Load<SpriteFont>("UI/Fonts/BebasNeueFont");
    }

    public override void Update(GameTime gameTime)
    {
        // If Enter is pressed, transition to the game state
        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
        {
            _stateManager.SetState(new PlayingState(_stateManager, _graphicsDevice, _content));
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // Draw background covering the entire screen
        spriteBatch.Draw(_backgroundTexture,
            new Rectangle(0, 0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height),
            Color.White);

        // Adjust game title position and size
        Vector2 titleSize = _titleFont.MeasureString("BALL BALL") * 2; // Double the size
        spriteBatch.DrawString(_titleFont, "BALL BALL",
            new Vector2((_graphicsDevice.Viewport.Width - titleSize.X) / 2, 50), // Move higher
            Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f); // Scale text

        // Display "Press Enter to Start" below the title
        Vector2 enterSize = _titleFont.MeasureString("PRESS ENTER TO START");
        spriteBatch.DrawString(_titleFont, "PRESS ENTER TO START",
            new Vector2((_graphicsDevice.Viewport.Width - enterSize.X) / 2, 500),
            Color.White);
    }
}
