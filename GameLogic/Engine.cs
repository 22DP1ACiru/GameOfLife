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
        public Engine(IGame game)
        {
            this.game = game;
            this.iterationCount = 0;
            this.livingCellCount = 0;
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
                    Console.Write(field[i, j] ? Constants.LivingCell : Constants.DeadCell);
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
    }
}
