using System.Text;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Welcome to Conway's Game of Life!");

            int size = 0;
            while (true)
            {
                Console.Write("Please enter the size of the field (e.g., 10 for a 10x10 field): ");
                if (int.TryParse(Console.ReadLine(), out size) && size > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid positive integer.");
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
