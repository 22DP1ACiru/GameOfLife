namespace GameOfLife
{
    public interface IEngine
    {
        /// <summary>
        /// Updates the game state, including the field, iteration count, and living cell count.
        /// </summary>
        void UpdateGameState();

        /// <summary>
        /// Gets the current iteration count.
        /// </summary>
        int IterationCount { get; }

        /// <summary>
        /// Gets the current count of living cells.
        /// </summary>
        int LivingCellCount { get; }

        /// <summary>
        /// Gets the current state of the game field.
        /// </summary>
        bool[,] Field { get; }
    }
}