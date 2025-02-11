namespace GameOfLife
{
    public class Engine : IEngine
    {
        private readonly IGame game;
        private int iterationCount;
        private int livingCellCount;

        /// <summary>
        /// Initializes a new instance of the Engine class with the specified game.
        /// </summary>
        /// <param name="game">The game instance to manage.</param>
        public Engine(IGame game, int instanceIterationCount = 0)
        {
            this.game = game;
            this.iterationCount = instanceIterationCount; // Set to 0 by default for new games
        }

        /// <summary>
        /// Starts the game loop with the specified update speed.
        /// </summary>
        /// <param name="updateSpeed">The speed at which the game updates, in milliseconds.</param>
        public void Start(int updateSpeed)
        {
            while (true)
            {
                Console.Clear();
                UpdateCounts();
                PrintField();
                Console.WriteLine("Press 'S' to save the game, 'Q' to quit");

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.S)
                    {
                        Console.Clear();
                        SaveGame();
                    }
                    else if (key == ConsoleKey.Q)
                    {
                        Console.Clear();
                        Console.WriteLine("Quitting the game...");
                        return;
                    }
                }

                game.UpdateField();
                Thread.Sleep(updateSpeed);
            }
        }

        /// <summary>
        /// Prints the current state of the game field to the console.
        /// </summary>
        private void PrintField()
        {
            bool[,] field = game.Field;
            int size = game.Size;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(field[i, j] ? DisplayConstants.LivingCell : DisplayConstants.DeadCell);
                }
                Console.WriteLine();
            }

            Console.WriteLine($"Iteration: {iterationCount}");
            Console.WriteLine($"Living cells: {livingCellCount}");
        }

        /// <summary>
        /// Updates the iteration count and living cell count.
        /// </summary>
        private void UpdateCounts()
        {
            iterationCount++;

            livingCellCount = 0;

            bool[,] field = game.Field;
            int size = game.Size;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (field[i, j])
                    {
                        livingCellCount++;
                    }
                }
            }
        }

        /// <summary>
        /// Saves the current game state to a JSON file.
        /// </summary>
        public void SaveGame()
        {
            Console.Write("Enter a name for the save file: ");
            string fileName = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(fileName))
            {
                var fileManager = new GameFileManager();
                fileManager.SaveGame(fileName, iterationCount, game.Size, game.Field);
            }
            else
            {
                Console.WriteLine("Invalid file name. Save canceled.");
            }
        }
    }
}
