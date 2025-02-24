using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife
{
    /// <summary>
    /// Manages multiple parallel game instances for mass simulation and testing.
    /// </summary>
    public class ParallelGameManager
    {
        private readonly ConsoleRenderer renderer;
        private readonly List<IEngine> games = new List<IEngine>();
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly object syncLock = new object();
        private readonly int totalGames;
        private readonly int gamesPerPage;
        private int currentPage = 0;
        private int multiGamePage = 0;
        private int multiGamesPerPage = 5;
        private bool inMultiGameView = false;
        private bool isPaused = false;
        private readonly object pauseLock = new object();
        private long iterations = 0;
        private long totalLivingCells = 0;

        /// <summary>
        /// Initializes a new instance of the ParallelGameManager class.
        /// </summary>
        /// <param name="renderer">The console renderer instance used for display.</param>
        /// <param name="totalGames">The total number of games to simulate.</param>
        /// <param name="gamesPerPage">The number of games to display per page.</param>
        public ParallelGameManager(ConsoleRenderer renderer, int totalGames = 1000, int gamesPerPage = 10)
        {
            this.renderer = renderer;
            this.totalGames = totalGames;
            this.gamesPerPage = gamesPerPage;
            InitializeGames();
        }

        /// <summary>
        /// Initializes the specified number of game instances with random sizes.
        /// </summary>
        private void InitializeGames()
        {
            Random random = new Random();
            for (int i = 0; i < totalGames; i++)
            {
                int size = random.Next(GameConstants.MinFieldSize, GameConstants.MaxFieldSize + 1);
                IGame game = new Game(size);
                IEngine engine = new Engine(game);
                games.Add(engine);
            }
        }

        /// <summary>
        /// Starts the parallel game simulation and manages user navigation.
        /// </summary>
        public void StartParallelSimulation()
        {
            Task simulationTask = Task.Run(() => RunParallelSimulation(cancellationTokenSource.Token));

            bool running = true;
            while (running && !cancellationTokenSource.IsCancellationRequested)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.N:
                            if (!inMultiGameView)
                            {
                                NextPage();
                            }
                            break;
                        case ConsoleKey.P:
                            if (!inMultiGameView)
                            {
                                PreviousPage();
                            }
                            break;
                        case ConsoleKey.M:
                            if (!inMultiGameView)
                            {
                                inMultiGameView = true;
                                multiGamePage = 0; // Reset to first page when entering multi-game view
                                ShowMultiGameDisplay();
                                inMultiGameView = false;
                            }
                            break;
                        case ConsoleKey.Spacebar:
                            lock (pauseLock)
                            {
                                isPaused = !isPaused;
                                Console.WriteLine(isPaused ? DisplayConstants.AllGamesPaused : DisplayConstants.AllGamesResumed);
                            }
                            break;

                        case ConsoleKey.S:
                            SaveAllGames();
                            break;

                        case ConsoleKey.L:
                            LoadAllGames();
                            break;
                        case ConsoleKey.Q:
                            Console.Clear();
                            running = false;
                            break;
                    }
                }
                Thread.Sleep(100); // Prevent CPU hogging in input loop
            }

            cancellationTokenSource.Cancel();
            try
            {
                simulationTask.Wait();
            }
            catch (AggregateException)
            {
                // Task was canceled, which is expected
            }
        }

        private void SaveAllGames()
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
                Console.WriteLine($"Error saving games: {ex.Message}");
            }
        }

        private void LoadAllGames()
        {
            try
            {
                string[] saveFiles = Directory.GetFiles(FileConstants.SaveFolder, "parallel_save_*.json");
                if (saveFiles.Length == 0)
                {
                    Console.WriteLine(DisplayConstants.NoSavedGamesFound);
                    return;
                }

                // Load the most recent save
                string filePath = saveFiles.OrderByDescending(f => f).First();
                string json = File.ReadAllText(filePath);
                var saveData = JsonConvert.DeserializeObject<ParallelSaveData>(json);

                games.Clear();
                foreach (var gameState in saveData.Games)
                {
                    IGame game = new Game(gameState.Size);
                    IEngine engine = new Engine(game, gameState.IterationCount);
                    games.Add(engine);
                }

                Console.WriteLine(DisplayConstants.AllGamesLoaded);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading games: {ex.Message}");
            }
        }

        /// <summary>
        /// Runs the parallel simulation for all game instances.
        /// </summary>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        private void RunParallelSimulation(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                lock (pauseLock)
                {
                    if (!isPaused)
                    {
                        totalLivingCells = 0;
                        // Update all games in parallel if unpaused
                        Parallel.ForEach(games, game =>
                        {
                            game.UpdateGameState();
                            Interlocked.Add(ref totalLivingCells, game.LivingCellCount);
                        });
                        iterations++;
                    }
                }

                // Only display current page if not in multi-game view
                if (!inMultiGameView)
                {
                    DisplayCurrentPage();
                }

                Thread.Sleep(GameConstants.ParallelGameUpdateSpeed);
            }
        }

        /// <summary>
        /// Displays the current page of game instances.
        /// </summary>
        private void DisplayCurrentPage()
        {
            lock (syncLock)
            {
                Console.Clear();
                int startIndex = currentPage * gamesPerPage;
                int endIndex = Math.Min(startIndex + gamesPerPage, totalGames);

                Console.WriteLine(DisplayConstants.ParallelGameHeaderFormat, currentPage + 1, (int)Math.Ceiling((double)totalGames / gamesPerPage));
                Console.WriteLine(DisplayConstants.NavigationInstructions);
                Console.WriteLine(DisplayConstants.ParallelGameControls);
                Console.WriteLine();
                Console.WriteLine(DisplayConstants.TotalStatisticsFormat, iterations, totalLivingCells);
                Console.WriteLine();

                var displayGames = games.Skip(startIndex).Take(gamesPerPage).ToList();

                for (int i = 0; i < displayGames.Count; i++)
                {
                    int gameId = startIndex + i + 1;
                    IEngine game = displayGames[i];
                    renderer.RenderGameSummary(gameId, game.IterationCount, game.LivingCellCount, game.Field.GetLength(0));
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Moves to the next page of games.
        /// </summary>
        private void NextPage()
        {
            lock (syncLock)
            {
                int totalPages = (int)Math.Ceiling((double)totalGames / gamesPerPage);
                currentPage = (currentPage + 1) % totalPages;
            }
        }

        /// <summary>
        /// Moves to the previous page of games.
        /// </summary>
        private void PreviousPage()
        {
            lock (syncLock)
            {
                int totalPages = (int)Math.Ceiling((double)totalGames / gamesPerPage);
                currentPage = (currentPage - 1 + totalPages) % totalPages;
            }
        }

        /// <summary>
        /// Displays multiple games simultaneously in a grid layout with pagination.
        /// </summary>
        public void ShowMultiGameDisplay()
        {
            CancellationTokenSource multiDisplayCts = new CancellationTokenSource();
            Task displayTask = Task.Run(() => RunMultiGameDisplay(multiDisplayCts.Token));

            // Wait for user to press navigation keys or Q to quit
            while (!multiDisplayCts.IsCancellationRequested)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.N:
                            lock (syncLock)
                            {
                                int totalPages = (int)Math.Ceiling((double)totalGames / multiGamesPerPage);
                                multiGamePage = (multiGamePage + 1) % totalPages;
                            }
                            break;
                        case ConsoleKey.P:
                            lock (syncLock)
                            {
                                int totalPages = (int)Math.Ceiling((double)totalGames / multiGamesPerPage);
                                multiGamePage = (multiGamePage - 1 + totalPages) % totalPages;
                            }
                            break;
                        case ConsoleKey.Q:
                            multiDisplayCts.Cancel();
                            break;
                    }
                }
                Thread.Sleep(100);
            }

            try
            {
                displayTask.Wait();
            }
            catch (AggregateException)
            {
                // Task was canceled, which is expected
            }
        }

        /// <summary>
        /// Runs the multi-game display showing games updating simultaneously with pagination.
        /// </summary>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        private void RunMultiGameDisplay(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // Get games from the current multi-game page
                int startIndex = multiGamePage * multiGamesPerPage;
                List<IEngine> displayGames = new List<IEngine>();
                List<int> gameIds = new List<int>();

                // Get up to 5 games for current page
                for (int i = 0; i < multiGamesPerPage; i++)
                {
                    int gameIndex = startIndex + i;
                    if (gameIndex < totalGames)
                    {
                        displayGames.Add(games[gameIndex]);
                        gameIds.Add(gameIndex + 1); // Add 1 to make game IDs 1-based
                    }
                }

                lock (syncLock)
                {
                    Console.Clear();

                    // Display pagination info
                    int totalPages = (int)Math.Ceiling((double)totalGames / multiGamesPerPage);
                    Console.WriteLine(DisplayConstants.MultiGameDisplayHeader);
                    Console.WriteLine($"Page {multiGamePage + 1} of {totalPages}");
                    Console.WriteLine(DisplayConstants.MultiGameNavigationInstructions);
                    Console.WriteLine();

                    // Split games for display layout
                    int topRowCount = Math.Min(3, displayGames.Count);
                    int bottomRowCount = displayGames.Count - topRowCount;

                    // Display top row
                    if (topRowCount > 0)
                    {
                        renderer.RenderMultipleGames(
                            displayGames.Take(topRowCount).ToList(),
                            gameIds.Take(topRowCount).ToList(),
                            DisplayLayout.TopRow
                        );
                    }

                    // Display section divider if we have games for both rows
                    if (bottomRowCount > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine(DisplayConstants.SectionDivider);
                        Console.WriteLine();

                        // Display bottom row
                        renderer.RenderMultipleGames(
                            displayGames.Skip(topRowCount).Take(bottomRowCount).ToList(),
                            gameIds.Skip(topRowCount).Take(bottomRowCount).ToList(),
                            DisplayLayout.BottomRow
                        );
                    }
                }

                Thread.Sleep(GameConstants.MultiGameUpdateSpeed);
            }
        }
    }
}