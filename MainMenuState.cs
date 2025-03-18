// MainMenuState.cs
// Represents the main menu screen, allowing the player to start the game.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class MainMenuState : GameState
{
    private Texture2D _backgroundTexture; // Background image for the main menu
    private SpriteFont _titleFont; // Font used for the menu title and instructions

    // Constructor initializes the main menu state
    public MainMenuState(GameStateManager stateManager, GraphicsDevice graphicsDevice, ContentManager content)
        : base(stateManager, graphicsDevice, content)
    {
    }

    // Loads menu assets, including background and fonts
    public override void Enter()
    {
        _backgroundTexture = _content.Load<Texture2D>("UI/Images/MainMenuBG");
        _titleFont = _content.Load<SpriteFont>("UI/Fonts/BebasNeueFont");
    }

    // Updates menu interactions, transitioning to the game when Enter is pressed
    public override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
        {
            _stateManager.SetState(new PlayingState(_stateManager, _graphicsDevice, _content)); // Start the game
        }
    }

    // Draws the main menu elements, including the title and start prompt
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // Draw background covering the entire screen
        spriteBatch.Draw(_backgroundTexture,
            new Rectangle(0, 0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height),
            Color.White);

        // Draw game title at the top of the screen, scaled up
        Vector2 titleSize = _titleFont.MeasureString("BALL BALL") * 2; // Double the size
        spriteBatch.DrawString(_titleFont, "BALL BALL",
            new Vector2((_graphicsDevice.Viewport.Width - titleSize.X) / 2, 50), // Position higher on screen
            Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f); // Scale text size

        // Display "Press Enter to Start" below the title
        Vector2 enterSize = _titleFont.MeasureString("PRESS ENTER TO START");
        spriteBatch.DrawString(_titleFont, "PRESS ENTER TO START",
            new Vector2((_graphicsDevice.Viewport.Width - enterSize.X) / 2, 500), // Position lower on screen
            Color.White);
    }
}
