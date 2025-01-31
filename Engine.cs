namespace GameOfLife
{
    public class Engine
    {
        private readonly Game game;

        /// <summary>
        /// Initializes a new instance of the Engine class with the specified game.
        /// </summary>
        /// <param name="game">The game instance to manage.</param>
        public Engine(Game game)
        {
            this.game = game;
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
        }
    }
}
