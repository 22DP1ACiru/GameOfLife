namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            GameConstants.SetConsoleEncoding();
            Console.WriteLine(DisplayConstants.WelcomeMessage);

            while (true)
            {
                Console.WriteLine(DisplayConstants.StartNewGame);
                Console.WriteLine(DisplayConstants.LoadSavedGame);
                Console.WriteLine(DisplayConstants.Quit);
                Console.Write(DisplayConstants.ChooseOption);
                
                if (Enum.TryParse(Console.ReadLine(), out DisplayConstants.MenuOption choice))
                {
                    switch (choice)
                    {
                        case DisplayConstants.MenuOption.StartNewGame:
                            Console.Clear();
                            int size = GameMenuManager.GetFieldSize();
                            IGame game = new Game(size);
                            IEngine engine = new Engine(game);
                            engine.Start(GameConstants.GameUpdateSpeed);
                            break;
                        case DisplayConstants.MenuOption.LoadSavedGame:
                            Console.Clear();
                            GameMenuManager.LoadSavedGame();
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

        
    }
}
