namespace GameOfLife
{
    public class GameManager
    {
        private readonly ConsoleRenderer renderer;
        private readonly SaveManager saver;

        public GameManager(ConsoleRenderer renderer, SaveManager saver)
        {
            this.renderer = renderer;
            this.saver = saver;
        }

        public void StartGame(IEngine engine, int updateSpeed)
        {
            while (true)
            {
                Console.Clear();
                engine.UpdateGameState();
                renderer.Render(engine.Field, engine.IterationCount, engine.LivingCellCount);
                Console.WriteLine("Press 'S' to save the game, 'Q' to quit");

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.S)
                    {
                        Console.Clear();
                        Console.Write("Enter a name for the save file: ");
                        string fileName = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(fileName))
                        {
                            saver.SaveGame(fileName, engine.IterationCount, engine.Field.GetLength(0), engine.Field);
                        }
                        else
                        {
                            Console.WriteLine("Invalid file name. Save canceled.");
                        }
                    }
                    else if (key == ConsoleKey.Q)
                    {
                        Console.Clear();
                        Console.WriteLine("Quitting the game...");
                        return;
                    }
                }

                Thread.Sleep(updateSpeed);
            }
        }
    }
}