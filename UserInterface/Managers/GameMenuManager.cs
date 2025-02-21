using System;
using System.IO;

namespace GameOfLife
{
    public class GameMenuManager
    {
        private readonly GameManager gameManager;
        private readonly ConsoleRenderer renderer;

        public GameMenuManager(GameManager gameManager, ConsoleRenderer renderer)
        {
            this.gameManager = gameManager;
            this.renderer = renderer;
        }

        public void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine(DisplayConstants.WelcomeMessage);
                Console.WriteLine(DisplayConstants.StartNewGame);
                Console.WriteLine(DisplayConstants.LoadSavedGame);
                Console.WriteLine(DisplayConstants.StartParallelSimulation);
                Console.WriteLine(DisplayConstants.Quit);
                Console.Write(DisplayConstants.ChooseOption);

                if (Enum.TryParse(Console.ReadLine(), out DisplayConstants.MenuOption choice))
                {
                    switch (choice)
                    {
                        case DisplayConstants.MenuOption.StartNewGame:
                            Console.Clear();
                            int size = GetFieldSize();
                            IGame game = new Game(size);
                            IEngine engine = new Engine(game);
                            gameManager.StartGame(engine, GameConstants.GameUpdateSpeed);
                            break;
                        case DisplayConstants.MenuOption.LoadSavedGame:
                            Console.Clear();
                            LoadSavedGame();
                            break;
                        case DisplayConstants.MenuOption.StartParallelSimulation:
                            Console.Clear();
                            ParallelGameManager parallelManager = new ParallelGameManager(renderer);
                            parallelManager.StartParallelSimulation();
                            break;
                        case DisplayConstants.MenuOption.Quit:
                            return;
                        default:
                            Console.Clear();
                            Console.WriteLine(DisplayConstants.InvalidChoice);
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(DisplayConstants.InvalidChoice);
                }
            }
        }

        private int GetFieldSize()
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

        private void LoadSavedGame()
        {
            if (!Directory.Exists(FileConstants.SaveFolder))
            {
                Console.WriteLine(DisplayConstants.NoSavedGamesFound);
                return;
            }

            string[] saveFiles = Directory.GetFiles(FileConstants.SaveFolder, "*.json");

            if (saveFiles.Length == 0)
            {
                Console.WriteLine(DisplayConstants.NoSavedGamesFound);
                return;
            }

            Console.WriteLine(DisplayConstants.SavedGamesList);
            for (int i = 0; i < saveFiles.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(saveFiles[i])}");
            }

            Console.Write(DisplayConstants.SavedGamesChoice);
            if (int.TryParse(Console.ReadLine(), out int selectedIndex) && selectedIndex > 0 && selectedIndex <= saveFiles.Length)
            {
                string filePath = saveFiles[selectedIndex - 1];
                IGame game = new Game(1); // Temporary size to be overwritten by LoadGame
                int iterationCount = game.LoadGame(filePath);
                IEngine engine = new Engine(game, iterationCount);
                gameManager.StartGame(engine, GameConstants.GameUpdateSpeed);
            }
            else
            {
                Console.Clear();
                Console.WriteLine(DisplayConstants.InvalidChoice);
            }
        }
    }
}