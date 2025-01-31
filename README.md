# Conway's Game of Life

Conway's Game of Life is a cellular automaton devised by the British mathematician John Horton Conway in 1970. It is a zero-player game, meaning that its evolution is determined by its initial state, requiring no further input. One interacts with the Game of Life by creating an initial configuration and observing how it evolves.

## How the Game Works

The game consists of a grid of cells, each of which can be in one of two states: alive or dead. The state of each cell changes from one generation to the next based on the following rules:

1. **Any live cell with fewer than two live neighbors dies** (as if by underpopulation).
2. **Any live cell with two or three live neighbors lives on** to the next generation.
3. **Any live cell with more than three live neighbors dies** (as if by overpopulation).
4. **Any dead cell with exactly three live neighbors becomes a live cell** (as if by reproduction).

This console application displays the Game of Life using a grid of emojis:
- `😊` represents a living cell.
- `💀` represents a dead cell.

The game updates every second by default, showing the evolution of the cells based on the rules mentioned above.

## Setup and Usage

### Prerequisites

- .NET 8 SDK

### Running the Application

1. **Clone the repository**
2. **Open in any .NET supported IDE**
3. **Build and run the project**
4. **Follow the on-screen instructions**
5. **Enjoy the Game of Life!**

### Using the Application

1. **Start the application**: When you run the application, you will be greeted with a welcome message.

2. **Enter the field size**: You will be prompted to enter the size of the field. The size must be a positive integer between 5 and 40.

3. **Observe the game**: The game will start with a randomly initialized field. The console will clear and update the field every specified interval, showing the evolution of the cells.
