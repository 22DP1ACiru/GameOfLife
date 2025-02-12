namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            GameConstants.SetConsoleEncoding();

            ConsoleRenderer renderer = new ConsoleRenderer();
            SaveManager saver = new SaveManager();
            GameManager gameManager = new GameManager(renderer, saver);
            GameMenuManager menuManager = new GameMenuManager(gameManager);

            menuManager.ShowMainMenu();
        }
    }
}