namespace GameOfLife
{
    public interface IGame
    {
        /// <summary>
        /// Gets the current state of the game field.
        /// </summary>
        bool[,] Field { get; }

        /// <summary>
        /// Gets the size of the game field.
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Updates the game field based on the rules of the Game of Life.
        /// </summary>
        /// <returns>A new 2D boolean array representing the updated game field.</returns>
        bool[,] UpdateField();
    }
}
