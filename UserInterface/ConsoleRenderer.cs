namespace GameOfLife
{
    public class ConsoleRenderer
    {
        /// <summary>
        /// Renders the current state of the game field to the console.
        /// </summary>
        /// <param name="field">The game field to render.</param>
        /// <param name="iterationCount">The current iteration count.</param>
        /// <param name="livingCellCount">The current count of living cells.</param>
        public void Render(bool[,] field, int iterationCount, int livingCellCount)
        {
            int size = field.GetLength(0);

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
    }
}