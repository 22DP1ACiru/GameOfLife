using Newtonsoft.Json;

namespace GameOfLife
{
    public class SaveManager
    {
        private readonly GameFileManager fileManager = new GameFileManager();

        /// <summary>
        /// Saves a single game state to a JSON file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="iterationCount"></param>
        /// <param name="size"></param>
        /// <param name="field"></param>
        public void SaveGame(string fileName, int iterationCount, int size, bool[,] field)
        {
            fileManager.SaveGame(fileName, iterationCount, size, field);
        }

        /// <summary>
        /// Loads a single game state from a JSON file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public SaveData LoadGame(string filePath)
        {
            return fileManager.LoadGame(filePath);
        }

        /// <summary>
        /// Detects if a save file contains parallel games.
        /// </summary>
        public bool IsParallelSave(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                var saveData = JsonConvert.DeserializeObject<ParallelSaveData>(json);
                return saveData != null && saveData.Games != null && saveData.Games.Count > 1;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Loads a parallel game save file.
        /// </summary>
        public ParallelSaveData LoadParallelGames(string filePath)
        {
            try
            {
                return fileManager.LoadParallelGames(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading parallel games: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Saves all parallel games.
        /// </summary>
        public void SaveParallelGames(List<IEngine> games)
        {
            fileManager.SaveParallelGames(games);
        }
    }
}