using System;

namespace Ball_Ball
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using var game = new GameManager(); // Initialize the game
            game.Run();  // Start the game loop
        }
    }
}
