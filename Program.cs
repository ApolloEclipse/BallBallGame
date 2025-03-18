// Program.cs
// Entry point of the game, initializing and starting the game loop.

using System;

namespace Ball_Ball
{
    public static class Program
    {
        [STAThread] // Required for MonoGame to manage the game window properly
        static void Main()
        {
            using var game = new GameManager(); // Create and initialize the game
            game.Run(); // Start the game loop
        }
    }
}
