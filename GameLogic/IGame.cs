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

        /// <summary>
        /// Saves the current game state to a JSON file.
        /// </summary>
        /// <param name="fileName">The name of the save file (without extension).</param>
        /// <param name="iterationCount">The current iteration count.</param>
        void SaveGame(string fileName, int iterationCount);

        /// <summary>
        /// Loads a game state from a JSON file.
        /// </summary>
        /// <param name="filePath">The path to the save file.</param>
        /// <returns>The iteration count from the save file.</returns>
        int LoadGame(string filePath);
    }
}
