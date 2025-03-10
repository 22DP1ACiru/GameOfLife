﻿using Newtonsoft.Json;

namespace GameOfLife
{
    public class Game : IGame
    {
        private readonly GameFileManager fileManager = new GameFileManager();
        private bool[,] field;
        private int size;

        /// <summary>
        /// Initializes a new instance of the Game class with the specified field size.
        /// </summary>
        /// <param name="size">The size of the field (NxN).</param>
        public Game(int size)
        {
            this.size = size;
            field = InitializeField(size);
        }

        /// <summary>
        /// Gets the current state of the game field.
        /// </summary>
        public bool[,] Field => field;

        /// <summary>
        /// Gets the size of the game field.
        /// </summary>
        public int Size => size;

        /// <summary>
        /// Initializes the game field with random live and dead cells.
        /// </summary>
        /// <param name="size">The size of the field (NxN), inputted by user.</param>
        /// <returns>A 2D boolean array representing the game field.</returns>
        private bool[,] InitializeField(int size)
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
        /// Updates the game field based on the rules of the Game of Life.
        /// </summary>
        /// <returns>A new 2D boolean array representing the updated game field.</returns>
        public bool[,] UpdateField()
        {
            bool[,] newField = new bool[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int liveNeighbors = CountLiveNeighbors(i, j);
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

            field = newField;
            return field;
        }

        /// <summary>
        /// Counts the number of live neighbors for a given cell.
        /// </summary>
        /// <param name="x">The x-coordinate of the cell.</param>
        /// <param name="y">The y-coordinate of the cell.</param>
        /// <returns>The number of live neighbors.</returns>
        private int CountLiveNeighbors(int x, int y)
        {
            int liveNeighbors = 0;

            // iterating over a 3x3 grid centered at the cell specified in the arguments
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    // skips the specified cell
                    if (i == 0 && j == 0) continue;

                    int nx = x + i;
                    int ny = y + j;

                    // checks if the neighbor is within the bounds of the field and is alive
                    if (nx >= 0 && nx < size && ny >= 0 && ny < size && field[nx, ny])
                    {
                        liveNeighbors++;
                    }
                }
            }

            return liveNeighbors;
        }

        /// <summary>
        /// Loads a game state from a JSON file.
        /// </summary>
        /// <param name="filePath">The path to the save file.</param>
        /// <returns>The iteration count from the save file.</returns>
        public int LoadGame(string filePath)
        {
            // Load the game state from file using GameFileManager
            var saveData = fileManager.LoadGame(filePath);

            // Update the game state
            size = saveData.Size;
            field = saveData.Field;

            // Return the iteration count
            return saveData.IterationCount;
        }

        /// <summary>
        /// Loads a game state from memory.
        /// </summary>
        /// <param name="state"></param>
        public void LoadFromState(GameState state)
        {
            this.size = state.Size;
            this.field = state.Field;
        }
    }
}