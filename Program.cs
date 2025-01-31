using System.Text;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            // setting the console output encoding to UTF-8 to display emojis correctly
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Welcome to Conway's Game of Life!");

            int size = 0;
            while (true)
            {
                Console.Write("Please enter the size of the field (minimum 5, maximum 40): ");
                if (int.TryParse(Console.ReadLine(), out size) && size >= 5 && size <= 40)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid positive integer between 5 and 40.");
                }
            }

            Console.WriteLine($"You have selected a {size}x{size} field.");

            bool[,] field = InitializeField(size);
            while (true)
            {
                Console.Clear();
                PrintField(field);
                field = UpdateField(field);
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Initializes the game field with random live and dead cells.
        /// </summary>
        /// <param name="size">The size of the field (NxN), inputted by user.</param>
        /// <returns>A 2D boolean array representing the game field.</returns>
        static bool[,] InitializeField(int size)
        {
            bool[,] field = new bool[size, size];
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    field[i, j] = random.Next(2) == 0;
                }
            }
            return field;
        }

        /// <summary>
        /// Prints the current state of the game field to the console.
        /// </summary>
        /// <param name="field">The game field to print.</param>
        static void PrintField(bool[,] field)
        {
            int size = field.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(field[i, j] ? "😊" : "💀");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Updates the game field based on the rules of Conway's Game of Life.
        /// </summary>
        /// <param name="field">The current game field.</param>
        /// <returns>A new game field after applying the rules.</returns>
        static bool[,] UpdateField(bool[,] field)
        {
            int size = field.GetLength(0);
            bool[,] newField = new bool[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int liveNeighbors = CountLiveNeighbors(field, i, j);
                    if (field[i, j])
                    {
                        newField[i, j] = liveNeighbors == 2 || liveNeighbors == 3;
                    }
                    else
                    {
                        newField[i, j] = liveNeighbors == 3;
                    }
                }
            }

            return newField;
        }

        /// <summary>
        /// Counts the number of live neighbors for a given cell.
        /// </summary>
        /// <param name="field">The game field.</param>
        /// <param name="x">The x-coordinate of the cell.</param>
        /// <param name="y">The y-coordinate of the cell.</param>
        /// <returns>The number of live neighbors.</returns>
        static int CountLiveNeighbors(bool[,] field, int x, int y)
        {
            int size = field.GetLength(0);
            int liveNeighbors = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;

                    int nx = x + i;
                    int ny = y + j;

                    if (nx >= 0 && nx < size && ny >= 0 && ny < size && field[nx, ny])
                    {
                        liveNeighbors++;
                    }
                }
            }

            return liveNeighbors;
        }
    }
}
