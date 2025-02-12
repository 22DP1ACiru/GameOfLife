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
        /// Updates game state - field, iteration count, and living cell count.
        /// </summary>  
        public void UpdateGameState()
        {
            iterationCount++;
            livingCellCount = CountLivingCells();
            game.UpdateField();
        }

        public int IterationCount => this.iterationCount;
        public int LivingCellCount => this.livingCellCount;
        public bool[,] Field => this.game.Field;

        /// <summary>
        /// Returns the current living cell count.
        /// </summary>
        private int CountLivingCells()
        {
            int count = 0;
            bool[,] field = game.Field;
            int size = game.Size;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (field[i, j])
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}