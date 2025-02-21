using System.Text;

namespace GameOfLife
{
    public enum DisplayLayout
    {
        FullScreen,
        TopRow,
        BottomRow
    }

    public class ConsoleRenderer
    {
        private readonly StringBuilder buffer = new StringBuilder();

        /// <summary>
        /// Renders the current state of the game field to the console with buffer optimization.
        /// </summary>
        /// <param name="field">The game field to render.</param>
        /// <param name="iterationCount">The current iteration count.</param>
        /// <param name="livingCellCount">The current count of living cells.</param>
        public void Render(bool[,] field, int iterationCount, int livingCellCount)
        {
            buffer.Clear();
            int size = field.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    buffer.Append(field[i, j] ? DisplayConstants.LivingCell : DisplayConstants.DeadCell);
                }
                buffer.AppendLine();
            }

            buffer.AppendLine($"Iteration: {iterationCount}");
            buffer.AppendLine($"Living cells: {livingCellCount}");

            Console.Clear();
            Console.Write(buffer.ToString());
        }

        /// <summary>
        /// Renders a compact summary of a game instance.
        /// </summary>
        /// <param name="gameId">The ID of the game.</param>
        /// <param name="iterationCount">The current iteration count.</param>
        /// <param name="livingCellCount">The current count of living cells.</param>
        /// <param name="size">The size of the game field.</param>
        public void RenderGameSummary(int gameId, int iterationCount, int livingCellCount, int size)
        {
            buffer.Clear();
            buffer.AppendFormat(DisplayConstants.GameSummaryFormat,
                gameId, size, iterationCount, livingCellCount);
            Console.Write(buffer.ToString());
        }

        /// <summary>
        /// Renders multiple games side by side in a specified layout.
        /// </summary>
        /// <param name="engines">List of game engines to render.</param>
        /// <param name="gameIds">List of actual game IDs for proper labeling.</param>
        /// <param name="layout">The display layout to use.</param>
        public void RenderMultipleGames(List<IEngine> engines, List<int> gameIds, DisplayLayout layout)
        {
            buffer.Clear();

            // Determine display constraints based on layout
            int maxHeight = layout == DisplayLayout.FullScreen ? 20 : 10;
            int maxWidth = Console.WindowWidth / engines.Count;

            // Create game field representations in memory
            List<string[]> gameRepresentations = new List<string[]>();
            for (int gameIndex = 0; gameIndex < engines.Count; gameIndex++)
            {
                var engine = engines[gameIndex];
                var field = engine.Field;
                int size = field.GetLength(0);
                int actualGameId = gameIds[gameIndex];

                // Scale the field to fit in console
                int scaleFactor = Math.Max(1, Math.Max(1, size) / Math.Max(1, Math.Min(maxHeight, maxWidth)));
                int scaledSize = size / scaleFactor;

                string[] lines = new string[scaledSize + 3]; // Field + stats lines

                // Render field with scaling
                for (int i = 0; i < scaledSize; i++)
                {
                    StringBuilder lineBuilder = new StringBuilder();
                    for (int j = 0; j < scaledSize; j++)
                    {
                        // Handle edge cases where scaling might cause index issues
                        int fieldI = Math.Min(i * scaleFactor, size - 1);
                        int fieldJ = Math.Min(j * scaleFactor, size - 1);

                        bool cellValue = field[fieldI, fieldJ];
                        lineBuilder.Append(cellValue ? DisplayConstants.LivingCell : DisplayConstants.DeadCell);
                    }
                    lines[i] = lineBuilder.ToString().PadRight(maxWidth);
                }

                // Add stats
                lines[scaledSize] = $"Game ID: {actualGameId}".PadRight(maxWidth);
                lines[scaledSize + 1] = $"Iteration: {engine.IterationCount}".PadRight(maxWidth);
                lines[scaledSize + 2] = $"Living: {engine.LivingCellCount}".PadRight(maxWidth);

                gameRepresentations.Add(lines);
            }

            // Calculate the maximum number of lines across all games
            int maxLines = 0;
            foreach (var rep in gameRepresentations)
            {
                maxLines = Math.Max(maxLines, rep.Length);
            }

            // Render games side-by-side
            for (int lineIndex = 0; lineIndex < maxLines; lineIndex++)
            {
                for (int gameIndex = 0; gameIndex < gameRepresentations.Count; gameIndex++)
                {
                    var rep = gameRepresentations[gameIndex];
                    string line = lineIndex < rep.Length ? rep[lineIndex] : new string(' ', maxWidth);
                    buffer.Append(line);
                    buffer.Append("  "); // Space between games
                }
                buffer.AppendLine();
            }

            Console.Write(buffer.ToString());
        }
    }
}