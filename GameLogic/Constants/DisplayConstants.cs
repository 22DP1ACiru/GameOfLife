namespace GameOfLife
{
    public static class DisplayConstants
    {
        public enum MenuOption
        {
            StartNewGame = 1,
            LoadSavedGame = 2,
            Quit = 3
        }

        public const string StartNewGame = "1. Start a new game";
        public const string LoadSavedGame = "2. Load a saved game";
        public const string Quit = "3. Quit";
        public const string ChooseOption = "Choose an option: ";
        public const string InvalidChoice = "Invalid choice. Please try again.";
        public const string WelcomeMessage = "Welcome to Conway's Game of Life!";
        public const string NoSavedGamesFound = "No saved games found.";
        public const string SavedGamesList = "Saved games:";
        public const string SavedGamesChoice = "Select a save file by number: ";

        /// <summary>
        /// The representation of a living cell in the game field.
        /// </summary>
        public const string LivingCell = "😊";

        /// <summary>
        /// The representation of a dead cell in the game field.
        /// </summary>
        public const string DeadCell = "💀";
    }
}