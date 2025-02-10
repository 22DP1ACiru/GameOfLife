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
            // Ensure the save directory exists (specified in Constants)
            Directory.CreateDirectory(Constants.SaveFolder);

            var saveData = new SaveData
            {
                Size = size,
                Field = field,
                IterationCount = iterationCount
            };

            // Generate the file name using file format in Constants
            string filePath = Path.Combine(Constants.SaveFolder, string.Format(Constants.SaveFileNameFormat, fileName, DateTime.Now));

            // Serialize and save to JSON
            string json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
            File.WriteAllText(filePath, json);

            Console.WriteLine($"Game saved successfully to {filePath}!");
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
    }
}