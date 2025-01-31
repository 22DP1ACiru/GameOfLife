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

            int size = GetFieldSize();

            IGame game = new Game(size);
            IEngine engine = new Engine(game);
            engine.Start(Constants.GameUpdateSpeed);
        }

        /// <summary>
        /// Prompts the user to enter the size of the field and validates the input.
        /// </summary>
        /// <returns>The size of the field.</returns>
        private static int GetFieldSize()
        {
            int size = 0;
            while (true)
            {
                Console.Write($"Please enter the size of the field (minimum {Constants.MinFieldSize}, maximum {Constants.MaxFieldSize}): ");
                if (int.TryParse(Console.ReadLine(), out size) && size >= Constants.MinFieldSize && size <= Constants.MaxFieldSize)
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Invalid input. Please enter a valid positive integer between {Constants.MinFieldSize} and {Constants.MaxFieldSize}.");
                }
            }
            return size;
        }
    }
}
