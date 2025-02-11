namespace GameOfLife
{
    public class GameMenuManager
    {
        /// <summary>
        /// Prompts the user to select a saved game from the "saves/" folder.
        /// </summary>
        public static void LoadSavedGame()
        {
            // Ensure the save directory exists (specified in Constants)
            if (!Directory.Exists(FileConstants.SaveFolder))
            {
                Console.WriteLine(DisplayConstants.NoSavedGamesFound);
                return;
            }

            // Get all .json files in the "saves" folder
            string[] saveFiles = Directory.GetFiles(FileConstants.SaveFolder, "*.json");

            if (saveFiles.Length == 0)
            {
                Console.WriteLine(DisplayConstants.NoSavedGamesFound);
                return;
            }

            // List all save files
            Console.WriteLine(DisplayConstants.SavedGamesList);
            for (int i = 0; i < saveFiles.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(saveFiles[i])}");
            }

            // Prompt the user to select a save file
            Console.Write(DisplayConstants.SavedGamesChoice);
            if (int.TryParse(Console.ReadLine(), out int selectedIndex) && selectedIndex > 0 && selectedIndex <= saveFiles.Length)
            {
                string filePath = saveFiles[selectedIndex - 1];
                IGame game = new Game(1); // Temporary size to be overwritten by LoadGame later
                int iterationCount = game.LoadGame(filePath); // Load the game and get the iteration count
                IEngine engine = new Engine(game, iterationCount);
                engine.Start(GameConstants.GameUpdateSpeed);
            }
            else
            {
                Console.Clear();
                Console.WriteLine(DisplayConstants.InvalidChoice);
            }
        }

        /// <summary>
        /// Prompts the user to enter the size of the field and validates the input.
        /// </summary>
        /// <returns>The size of the field.</returns>
        public static int GetFieldSize()
        {
            int size = 0;
            while (true)
            {
                Console.Write($"Please enter the size of the field (minimum {GameConstants.MinFieldSize}, maximum {GameConstants.MaxFieldSize}): ");
                if (int.TryParse(Console.ReadLine(), out size) && size >= GameConstants.MinFieldSize && size <= GameConstants.MaxFieldSize)
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Invalid input. Please enter a valid positive integer between {GameConstants.MinFieldSize} and {GameConstants.MaxFieldSize}.");
                }
            }
            return size;
        }
    }
}