using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GridExemplarWPF
{
    /*
     * GridRenderer - WPF adaptation of GridRendererConsole
     *
     * TRANSITIONING FROM CONSOLE TO WPF:
     * If you've built a console prototype (see GridRendererConsole.cs), you already
     * know how to render a grid! This class does THE SAME THING, just with different
     * output methods.
     *
     * WHAT STAYED THE SAME:
     *  - Takes a Square[,] grid as input (your data structure doesn't change!)
     *  - Loops through rows and columns (same algorithm structure)
     *  - Checks wall properties (NorthWall, EastWall, etc.) to decide what to draw
     *  - Has a DrawGrid() method that renders everything
     *
     * WHAT CHANGED:
     *  - Console: Console.Write("─") → WPF: canvas.Children.Add(new Line(...))
     *  - Console: Text characters → WPF: Graphical shapes (Line, Rectangle)
     *  - Console: Console.Clear() → WPF: canvas.Children.Clear()
     *  - WPF: Need to specify pixel coordinates and colors explicitly
     *  - WPF: Constructor takes a Canvas to draw on
     *
     * WHY SEPARATE FROM MainWindow.xaml.cs?
     * Same reason as the console version - separation of concerns!
     *  - MainWindow handles events and state
     *  - GridRenderer handles drawing
     *  - Makes code easier to test, debug, and extend
     */
    public sealed class GridRenderer
    {
        private readonly Canvas canvas;

        /// <summary>
        /// Creates a renderer that draws to the provided WPF Canvas.
        /// DIFFERENCE FROM CONSOLE: Console version didn't need a constructor - it just
        /// wrote to Console.Out. WPF needs to know WHICH canvas to draw on.
        /// </summary>
        /// <param name="canvas">The WPF Canvas control where shapes will be added.</param>
        public GridRenderer(Canvas canvas)
        {
            this.canvas = canvas;
        }

        /// <summary>
        /// Main rendering method - clears the canvas and draws the entire grid.
        /// CONSOLE EQUIVALENT: This is like GridRendererConsole.DrawGrid()
        /// Same parameters, same logic flow, different output method.
        /// </summary>
        /// <param name="grid">The 2D array of Square objects to render.</param>
        /// <param name="showGridLines">If false, only filled rectangles are drawn (no wall lines).</param>
        public void DrawGrid(Square[,] grid, bool showGridLines)
        {
            // Clear previous drawing
            // CONSOLE: Used Console.Clear()
            // WPF: Remove all child shapes from the canvas
            canvas.Children.Clear();

            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            // Same nested loop structure as console version!
            // We're iterating through the same data structure
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    DrawSquare(grid[row, col], showGridLines);
                }
            }
        }

        /// <summary>
        /// Draws one square: a filled background plus (optionally) any walls that exist.
        /// CONSOLE EQUIVALENT: Console version checked IsSelected and printed " X " or "   "
        /// WPF: We create actual Rectangle and Line objects with colors.
        /// </summary>
        /// <param name="square">The square to draw.</param>
        /// <param name="showGridLines">If false, no wall lines are drawn.</param>
        private void DrawSquare(Square square, bool showGridLines)
        {
            // Determine colors based on selection state
            // CONSOLE: Changed output character (X vs space)
            // WPF: Change fill and stroke colors
            Brush lineColour = square.IsSelected ? Brushes.Red : Brushes.Black;
            Brush fillColour = square.IsSelected ? Brushes.LightCoral : Brushes.White;

            // Create a filled rectangle for the cell background
            // CONSOLE: This was the "   " (three spaces) in the middle of the cell
            // WPF: An actual Rectangle shape with width, height, and fill color
            Rectangle background = new Rectangle
            {
                Width = square.Size,
                Height = square.Size,
                Fill = fillColour
            };

            // Position the rectangle on the canvas using the square's X and Y coordinates
            // CONSOLE: Position was determined by which row/column we were printing
            // WPF: Explicitly set pixel coordinates
            Canvas.SetLeft(background, square.X);
            Canvas.SetTop(background, square.Y);
            canvas.Children.Add(background);

            // Skip wall drawing if gridlines are hidden (like console's simple mode)
            if (!showGridLines)
            {
                return;
            }

            // Draw each wall if it exists - SAME LOGIC AS CONSOLE!
            // CONSOLE: if (square.NorthWall) Console.Write("───");
            // WPF: if (square.NorthWall) create and add a Line shape
            
            if (square.NorthWall)
            {
                // Create a horizontal line across the top of the square
                // CONSOLE: Printed "───" (3 horizontal bar characters)
                // WPF: Create Line from top-left to top-right corner
                Line northLine = new Line
                {
                    X1 = square.X,                  // Start: top-left X
                    Y1 = square.Y,                  // Start: top-left Y
                    X2 = square.X + square.Size,    // End: top-right X
                    Y2 = square.Y,                  // End: top-right Y (same Y = horizontal)
                    Stroke = lineColour,
                    StrokeThickness = 2
                };
                canvas.Children.Add(northLine);
            }

            if (square.EastWall)
            {
                // Create a vertical line down the right side of the square
                // CONSOLE: Printed "│" at the end of the cell
                // WPF: Create Line from top-right to bottom-right corner
                Line eastLine = new Line
                {
                    X1 = square.X + square.Size,    // Start: top-right X
                    Y1 = square.Y,                  // Start: top-right Y
                    X2 = square.X + square.Size,    // End: bottom-right X (same X = vertical)
                    Y2 = square.Y + square.Size,    // End: bottom-right Y
                    Stroke = lineColour,
                    StrokeThickness = 2
                };
                canvas.Children.Add(eastLine);
            }

            if (square.SouthWall)
            {
                // Create a horizontal line across the bottom of the square
                // CONSOLE: Printed "───" in the bottom border row
                // WPF: Create Line from bottom-right to bottom-left corner
                Line southLine = new Line
                {
                    X1 = square.X + square.Size,    // Start: bottom-right X
                    Y1 = square.Y + square.Size,    // Start: bottom-right Y
                    X2 = square.X,                  // End: bottom-left X
                    Y2 = square.Y + square.Size,    // End: bottom-left Y (same Y = horizontal)
                    Stroke = lineColour,
                    StrokeThickness = 2
                };
                canvas.Children.Add(southLine);
            }

            if (square.WestWall)
            {
                // Create a vertical line down the left side of the square
                // CONSOLE: Printed "│" at the start of each cell
                // WPF: Create Line from bottom-left to top-left corner
                Line westLine = new Line
                {
                    X1 = square.X,                  // Start: bottom-left X
                    Y1 = square.Y + square.Size,    // Start: bottom-left Y
                    X2 = square.X,                  // End: top-left X (same X = vertical)
                    Y2 = square.Y,                  // End: top-left Y
                    Stroke = lineColour,
                    StrokeThickness = 2
                };
                canvas.Children.Add(westLine);
            }
        }
    }
}