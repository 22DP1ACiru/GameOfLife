namespace GameOfLife
{
    public class SaveManager
    {
        private readonly GameFileManager fileManager = new GameFileManager();

        public void SaveGame(string fileName, int iterationCount, int size, bool[,] field)
        {
            fileManager.SaveGame(fileName, iterationCount, size, field);
        }

        public SaveData LoadGame(string filePath)
        {
            return fileManager.LoadGame(filePath);
        }
    }
}