using Newtonsoft.Json;

namespace GameOfLife
{
    public class GameFileManager
    {
        /// <summary>
        /// Saves the current game state to a JSON file.
        /// </summary>
        public void SaveGame(string fileName, int iterationCount, int size, bool[,] field)
        {
            try
            {
                // Ensure the save directory exists (specified in Constants)
                Directory.CreateDirectory(FileConstants.SaveFolder);

                var saveData = new SaveData
                {
                    Size = size,
                    Field = field,
                    IterationCount = iterationCount
                };

                // Generate the file name using file format in Constants
                string filePath = Path.Combine(FileConstants.SaveFolder, string.Format(FileConstants.SaveFileNameFormat, fileName, DateTime.Now));

                // Serialize and save to JSON
                string json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
                File.WriteAllText(filePath, json);

                Console.WriteLine($"Game saved successfully to {filePath}!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Failed to save the game. {ex.Message}");
            }
        }

        /// <summary>
        /// Loads a game state from a JSON file.
        /// </summary>
        public SaveData LoadGame(string filePath)
        {
            try
            {
                // Read and return deserialized JSON file
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<SaveData>(json);
            }
            catch (FileNotFoundException)
            {
                // Throws if file is not found (i.e. removed right before selection in load screen)
                Console.WriteLine("Error: Save file not found.");
                throw;
            }
            catch (JsonException)
            {
                // Throws if file is not valid JSON (corrupt or tampered with)
                Console.WriteLine("Error: Invalid save file format.");
                throw;
            }
        }

        /// <summary>
        /// Saves all parallel games to a single file.
        /// </summary>
        public void SaveParallelGames(List<IEngine> games)
        {
            try
            {
                var saveData = new ParallelSaveData
                {
                    Games = games.Select(g => new GameState
                    {
                        Field = g.Field,
                        IterationCount = g.IterationCount,
                        Size = g.Field.GetLength(0)
                    }).ToList()
                };

                string filePath = Path.Combine(FileConstants.SaveFolder,
                    string.Format(FileConstants.ParallelSaveFileNameFormat, DateTime.Now));

                string json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
                File.WriteAllText(filePath, json);

                Console.WriteLine(DisplayConstants.AllGamesSaved);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving parallel games: {ex.Message}");
            }
        }

        /// <summary>
        /// Loads all parallel games from a save file.
        /// </summary>
        public ParallelSaveData LoadParallelGames(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<ParallelSaveData>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading parallel games: {ex.Message}");
                throw;
            }
        }
    }
}