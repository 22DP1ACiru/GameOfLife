﻿using System.Text;

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
        public const int ParallelGameUpdateSpeed = 1500;

        /// <summary>
        /// The speed at which multi-game display updates, in milliseconds.
        /// </summary>
        public const int MultiGameUpdateSpeed = 800;

        /// <summary>
        /// Sets the console output encoding to UTF-8 to display emojis correctly.
        /// </summary>
        public static void SetConsoleEncoding()
        {
            Console.OutputEncoding = Encoding.UTF8;
        }
    }
}