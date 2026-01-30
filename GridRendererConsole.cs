using System;

namespace GridExemplarWPF
{
    /*
     * GridRendererConsole - Renders a grid to the console using text characters
     *
     * This class is responsible for taking a Square[,] grid (our data structure)
     * and displaying it on the console screen. This is a common approach in
     * command-line prototypes where you want to visualize your grid-based data.
     *
     * WHY SEPARATE RENDERING FROM MAIN LOGIC?
     * In a prototype, you might be tempted to put Console.Write() calls directly
     * in your main program or maze algorithm. DON'T! Keeping rendering separate:
     *  - Makes your code easier to test (you can check grid state without rendering)
     *  - Lets you change how things look without breaking your algorithms
     *  - Prepares you for moving to a GUI later (just swap the renderer!)
     *
     * HOW IT WORKS:
     * We use Unicode box-drawing characters to create walls:
     *  ─ = horizontal line    │ = vertical line
     *  ┌ = top-left corner    ┐ = top-right corner
     *  └ = bottom-left        ┘ = bottom-right
     *  ┬ = T-junction down    ┴ = T-junction up
     *
     * The algorithm loops through each row and column, checking each Square's
     * wall properties (NorthWall, EastWall, etc.) to decide which characters to print.
     */
    public sealed class GridRendererConsole
    {
        /// <summary>
        /// Main rendering method - clears the console and draws the entire grid.
        /// This method takes the grid data structure and converts it into visual output.
        /// </summary>
        /// <param name="grid">The 2D array of Square objects to render on screen.</param>
        /// <param name="showGridLines">True to show detailed walls, false for simple [X] marks.</param>
        public void DrawGrid(Square[,] grid, bool showGridLines)
        {
            Console.Clear();
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            if (showGridLines)
            {
                DrawGridWithWalls(grid, rows, cols);
            }
            else
            {
                DrawGridSimple(grid, rows, cols);
            }
        }

        /// <summary>
        /// Renders a detailed grid with walls visible using box-drawing characters.
        /// Each cell is 3 characters wide and 2 characters tall to accommodate walls.
        /// This is the complex version that shows the actual maze structure.
        /// </summary>
        private void DrawGridWithWalls(Square[,] grid, int rows, int cols)
        {
            // We need to render each row of cells in 2 console lines:
            // Line 1: Top borders (north walls)
            // Line 2: Side walls and cell content
            // After all rows, we add one more line for the bottom border
            for (int row = 0; row < rows; row++)
            {
                // STEP 1: Draw the top border for this entire row of cells
                for (int col = 0; col < cols; col++)
                {
                    Square square = grid[row, col];

                    // First column: draw corner character
                    if (col == 0)
                    {
                        Console.Write(square.NorthWall ? "┌" : " ");
                    }
                    else
                    {
                        // Between columns: use T-junction if either neighbor has a north wall
                        bool leftHasNorth = grid[row, col - 1].NorthWall;
                        bool thisHasNorth = square.NorthWall;
                        Console.Write((leftHasNorth || thisHasNorth) ? "┬" : " ");
                    }

                    // Draw horizontal line if this cell has a north wall
                    Console.Write(square.NorthWall ? "───" : "   ");
                }

                // End the top border line with the northeast corner
                Console.WriteLine(grid[row, cols - 1].NorthWall ? "┐" : " ");

                // STEP 2: Draw the content row (shows what's inside each cell)
                for (int col = 0; col < cols; col++)
                {
                    Square square = grid[row, col];

                    // Left side: draw west wall if it exists
                    Console.Write(square.WestWall ? "│" : " ");

                    // Center: show an X if this cell is selected, otherwise empty space
                    // This is where you could show other info (distances, path markers, etc.)
                    Console.Write(square.IsSelected ? " X " : "   ");

                    // Right side: only draw east wall for the last column
                    // (other east walls will be drawn as west walls of the next cell)
                    if (col == cols - 1)
                    {
                        Console.Write(square.EastWall ? "│" : " ");
                    }
                }
                Console.WriteLine();
            }

            // STEP 3: Draw the bottom border of the entire grid
            // This is similar to drawing top borders, but uses bottom corners
            for (int col = 0; col < cols; col++)
            {
                Square square = grid[rows - 1, col];

                // First column gets a corner
                if (col == 0)
                {
                    Console.Write(square.SouthWall ? "└" : " ");
                }
                else
                {
                    // Between columns: T-junction if either cell has a south wall
                    bool leftHasSouth = grid[rows - 1, col - 1].SouthWall;
                    bool thisHasSouth = square.SouthWall;
                    Console.Write((leftHasSouth || thisHasSouth) ? "┴" : " ");
                }

                // Horizontal line for south wall
                Console.Write(square.SouthWall ? "───" : "   ");
            }

            // Southeast corner of last cell
            Console.WriteLine(grid[rows - 1, cols - 1].SouthWall ? "┘" : " ");
        }

        /// <summary>
        /// Simple rendering mode - just shows which cells are selected.
        /// Useful for quick visualization or when walls don't matter.
        /// Each cell is shown as [X] if selected or [ ] if not.
        /// </summary>
        private void DrawGridSimple(Square[,] grid, int rows, int cols)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Console.Write(grid[row, col].IsSelected ? "[X]" : "[ ]");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Demonstration of how to use this renderer in a console application.
        /// This shows the typical pattern: create grid → populate data → render.
        /// </summary>
        public static void ExampleUsage()
        {
            // STEP 1: Define grid size
            const int SQUARE_SIZE = 40; // Used for positioning in the Square class
            int rows = 5;
            int cols = 5;

            // STEP 2: Create the 2D array to hold our grid
            Square[,] grid = new Square[rows, cols];
            Random random = new Random();

            // STEP 3: Initialize each square in the grid
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    // Calculate position (could be used for pixel coordinates later)
                    int x = col * SQUARE_SIZE;
                    int y = row * SQUARE_SIZE;
                    
                    // Create the square and give it random walls
                    grid[row, col] = new Square(x, y, SQUARE_SIZE);
                    grid[row, col].RandomiseWalls(random);
                }
            }

            // STEP 4: Mark a couple of cells as selected (just for demonstration)
            grid[1, 1].IsSelected = true;
            grid[3, 3].IsSelected = true;

            // STEP 5: Create a renderer and display the grid
            GridRendererConsole renderer = new GridRendererConsole();
            renderer.DrawGrid(grid, showGridLines: true);
            
            // That's it! The renderer reads the grid data and outputs it to console.
            // If you later move to WPF, this grid data structure works exactly the same.
        }
    }
}
