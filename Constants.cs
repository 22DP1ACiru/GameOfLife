using System.Text;

namespace GameOfLife
{
    public static class Constants
    {
        /// <summary>
        /// The minimum size of the game field (NxN).
        /// </summary>
        public const int MinFieldSize = 5;

        /// <summary>
        /// The maximum size of the game field (NxN).
        /// </summary>
        public const int MaxFieldSize = 40;

        /// <summary>
        /// The representation of a living cell in the game field.
        /// </summary>
        public const string LivingCell = "😊";

        /// <summary>
        /// The representation of a dead cell in the game field.
        /// </summary>
        public const string DeadCell = "💀";

        /// <summary>
        /// The speed at which the game updates, in milliseconds.
        /// </summary>
        public const int GameUpdateSpeed = 1000;

        /// <summary>
        /// The welcome message for the game.
        /// </summary>
        public const string WelcomeMessage = "Welcome to Conway's Game of Life!";

        /// <summary>
        /// Sets the console output encoding to UTF-8 to display emojis correctly.
        /// </summary>
        public static void SetConsoleEncoding()
        {
            Console.OutputEncoding = Encoding.UTF8;
        }
    }
}
