namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Constants.SetConsoleEncoding();
            Console.WriteLine(Constants.WelcomeMessage);

            while (true)
            {
                Console.WriteLine("1. Start a new game");
                Console.WriteLine("2. Load a saved game");
                Console.WriteLine("3. Quit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Clear();
                    int size = GetFieldSize();
                    IGame game = new Game(size);
                    IEngine engine = new Engine(game);
                    engine.Start(Constants.GameUpdateSpeed);
                }
                else if (choice == "2")
                {
                    Console.Clear();
                    LoadSavedGame();
                }
                else if (choice == "3")
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
        }

        /// <summary>
        /// Prompts the user to select a saved game from the "saves/" folder.
        /// </summary>
        private static void LoadSavedGame()
        {
            // Ensure the save directory exists (specified in Constants)
            Directory.CreateDirectory(Constants.SaveFolder);

            // Get all .json files in the "saves" folder
            string[] saveFiles = Directory.GetFiles(Constants.SaveFolder, "*.json");

            if (saveFiles.Length == 0)
            {
                Console.WriteLine("No saved games found.");
                return;
            }

            // List all save files
            Console.WriteLine("Saved games:");
            for (int i = 0; i < saveFiles.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(saveFiles[i])}");
            }

            // Prompt the user to select a save file
            Console.Write("Select a save file by number: ");
            if (int.TryParse(Console.ReadLine(), out int selectedIndex) && selectedIndex > 0 && selectedIndex <= saveFiles.Length)
            {
                string filePath = saveFiles[selectedIndex - 1];
                IGame game = new Game(1); // Temporary size to be overwritten by LoadGame later
                int iterationCount = game.LoadGame(filePath); // Load the game and get the iteration count
                IEngine engine = new Engine(game, iterationCount);
                engine.Start(Constants.GameUpdateSpeed);
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
        }

        /// <summary>
        /// Prompts the user to enter the size of the field and validates the input.
        /// </summary>
        /// <returns>The size of the field.</returns>
        private static int GetFieldSize()
        {
            int size = 0;
            while (true)
            {
                Console.Write($"Please enter the size of the field (minimum {Constants.MinFieldSize}, maximum {Constants.MaxFieldSize}): ");
                if (int.TryParse(Console.ReadLine(), out size) && size >= Constants.MinFieldSize && size <= Constants.MaxFieldSize)
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Invalid input. Please enter a valid positive integer between {Constants.MinFieldSize} and {Constants.MaxFieldSize}.");
                }
            }
            return size;
        }
    }
}
