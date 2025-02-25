using System.Text;

namespace GameOfLife
{
    public static class GameConstants
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
        /// The speed at which the game updates, in milliseconds.
        /// </summary>
        public const int GameUpdateSpeed = 1000;

        /// <summary>
        /// The speed at which parallel games update, in milliseconds.
        /// </summary>
        public const int ParallelGameUpdateSpeed = 1000;

        /// <summary>
        /// Default number of total parallel games to simulate.
        /// </summary>
        public const int DefaultTotalParallelGames = 1000;

        /// <summary>
        /// Default number of games to display per page in parallel simulation.
        /// </summary>
        public const int DefaultGamesPerPage = 10;

        /// <summary>
        /// Number of games to display per page in multi-game view.
        /// </summary>
        public const int MultiGamesPerPage = 8;

        /// <summary>
        /// Maximum number of games to display in the top row of multi-game view.
        /// </summary>
        public const int MultiGameTopRowCount = 3;

        /// <summary>
        /// Sets the console output encoding to UTF-8 to display emojis correctly.
        /// </summary>
        public static void SetConsoleEncoding()
        {
            Console.OutputEncoding = Encoding.UTF8;
        }
    }
}