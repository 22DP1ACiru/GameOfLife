namespace GameOfLife
{
    public static class DisplayConstants
    {
        public enum MenuOption
        {
            StartNewGame = 1,
            LoadSavedGame = 2,
            StartParallelSimulation = 3,
            Quit = 4
        }

        public const string StartNewGame = "1. Start a new game";
        public const string LoadSavedGame = "2. Load a saved game";
        public const string StartParallelSimulation = "3. Start 1000 parallel game test";
        public const string Quit = "4. Quit";
        public const string ChooseOption = "Choose an option: ";
        public const string InvalidChoice = "Invalid choice. Please try again.";
        public const string WelcomeMessage = "Welcome to Conway's Game of Life!";
        public const string NoSavedGamesFound = "No saved games found.";
        public const string SavedGamesList = "Saved games:";
        public const string SavedGamesChoice = "Select a save file by number: ";

        // Multi-game display constants
        public const string MultiGameDisplayHeader = "MULTI-GAME DISPLAY - GAMES RUNNING SIMULTANEOUSLY";
        public const string QuitInstruction = "Press 'Q' to return to the parallel simulation view";
        public const string SectionDivider = "------------------------------------------------";
        public const string NavigationInstructions = "Navigate: 'N' for next page, 'P' for previous page, 'M' for multi-game view, 'Q' to quit";
        public const string MultiGameNavigationInstructions = "Navigate: 'N' for next page, 'P' for previous page, 'Q' to return to main view";
        public const string ParallelGameHeaderFormat = "Parallel Game Simulation - Page {0} of {1}";
        public const string GameSummaryFormat = "Game ID: {0} | Size: {1}x{1} | Iteration: {2} | Living Cells: {3}";

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