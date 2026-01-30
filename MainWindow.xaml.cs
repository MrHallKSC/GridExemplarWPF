using System;
using System.Windows;
using System.Windows.Input;

namespace GridExemplarWPF
{
    public partial class MainWindow : Window
    {
        // Size of each square in pixels - kept constant for consistent display
        private const int SQUARE_SIZE = 40;

        // Grid dimensions - now variables instead of constants so they can change
        // These will be updated based on user input from the textboxes
        private int gridRows = 10;
        private int gridCols = 10;

        // 2D array to store all squares in the grid
        // We use a 2D array because it naturally represents rows and columns
        // This makes it easy to access a square at position [row, col]
        private Square[,] grid;

        // Boolean flag to control whether grid lines are drawn
        // This demonstrates how to store application state
        private bool showGridLines = true;

        // Random number generator - created once and reused
        // Creating it as a field ensures we get different random numbers each time
        // If we created new Random() inside a method called quickly, we'd get same numbers
        private readonly Random random = new Random();

        private readonly GridRenderer renderer;

        /// <summary>
        /// Initialises the WPF window, creates the renderer, and draws the initial grid.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent(); // built in method for WPF initialisation

            renderer = new GridRenderer(DrawingCanvas);

            InitialiseGrid(); // Set up the grid data structure
            Redraw(); // Render the grid on the canvas
            UpdateSelectedCount();  // Initialise the counter display
        }

        /// <summary>
        /// Triggers a full redraw of the current grid state.
        /// </summary>
        private void Redraw()
        {
            renderer.DrawGrid(grid, showGridLines);
        }

        /// <summary>
        /// (Re)creates the <see cref="Square"/> grid using the current <c>gridRows</c>/<c>gridCols</c>.
        /// Each square is created with random walls.
        /// </summary>
        private void InitialiseGrid()
        {
            grid = new Square[gridRows, gridCols];

            for (int row = 0; row < gridRows; row++)
            {
                for (int col = 0; col < gridCols; col++)
                {
                    int x = col * SQUARE_SIZE;
                    int y = row * SQUARE_SIZE;

                    grid[row, col] = new Square(x, y, SQUARE_SIZE);
                    grid[row, col].RandomiseWalls(random);
                }
            }
        }

        /// <summary>
        /// Handles left-mouse clicks on the canvas by toggling the selected state of the clicked square.
        /// </summary>
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point clickPoint = e.GetPosition(DrawingCanvas);

            for (int row = 0; row < gridRows; row++)
            {
                for (int col = 0; col < gridCols; col++)
                {
                    Square square = grid[row, col];

                    if (square.ContainsPoint(clickPoint.X, clickPoint.Y))
                    {
                        square.IsSelected = !square.IsSelected;

                        Redraw();
                        UpdateSelectedCount();
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Counts selected squares and updates the UI label to match.
        /// </summary>
        private void UpdateSelectedCount()
        {
            int count = 0;

            for (int row = 0; row < gridRows; row++)
            {
                for (int col = 0; col < gridCols; col++)
                {
                    if (grid[row, col].IsSelected)
                    {
                        count++;
                    }
                }
            }

            SelectedCountLabel.Text = count.ToString();
        }

        /// <summary>
        /// Validates the width/height textboxes, rebuilds the grid using those dimensions, and redraws.
        /// </summary>
        private void ApplyGridSize_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(WidthTextBox.Text, out int newWidth) &&
                int.TryParse(HeightTextBox.Text, out int newHeight))
            {
                if (newWidth >= 1 && newWidth <= 50 && newHeight >= 1 && newHeight <= 50)
                {
                    gridCols = newWidth;
                    gridRows = newHeight;

                    InitialiseGrid();
                    Redraw();
                    UpdateSelectedCount();
                }
                else
                {
                    MessageBox.Show(
                        "Please enter grid dimensions between 1 and 50.",
                        "Invalid Input",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning
                    );
                }
            }
            else
            {
                MessageBox.Show(
                    "Please enter valid numbers for width and height.",
                    "Invalid Input",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
            }
        }

        /// <summary>
        /// Randomizes walls for every square in the grid and redraws.
        /// </summary>
        private void RandomiseWalls_Click(object sender, RoutedEventArgs e)
        {
            for (int row = 0; row < gridRows; row++)
            {
                for (int col = 0; col < gridCols; col++)
                {
                    grid[row, col].RandomiseWalls(random);
                }
            }

            Redraw();
        }

        /// <summary>
        /// Clears selection on all squares and updates the UI.
        /// </summary>
        private void ClearSelection_Click(object sender, RoutedEventArgs e)
        {
            for (int row = 0; row < gridRows; row++)
            {
                for (int col = 0; col < gridCols; col++)
                {
                    grid[row, col].IsSelected = false;
                }
            }

            Redraw();
            UpdateSelectedCount();
        }

        /// <summary>
        /// Rebuilds the grid from scratch (new squares and new random walls) and updates the UI.
        /// </summary>
        private void ResetGrid_Click(object sender, RoutedEventArgs e)
        {
            InitialiseGrid();
            Redraw();
            UpdateSelectedCount();
        }

        /// <summary>
        /// Selects all squares and updates the UI.
        /// </summary>
        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            for (int row = 0; row < gridRows; row++)
            {
                for (int col = 0; col < gridCols; col++)
                {
                    grid[row, col].IsSelected = true;
                }
            }

            Redraw();
            UpdateSelectedCount();
        }

        /// <summary>
        /// Toggles visibility of grid/wall lines and redraws.
        /// </summary>
        private void ToggleGridLines_Click(object sender, RoutedEventArgs e)
        {
            showGridLines = !showGridLines;
            Redraw();
        }

        /// <summary>
        /// Exits the application by closing the main window.
        /// </summary>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Shows a dialog describing what the project demonstrates.
        /// </summary>
        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Grid Selection Example\n\n" +
                "Demonstrates:\n" +
                "- Drawing shapes with individual walls\n" +
                "- 2D arrays for grid structure\n" +
                "- Dynamic grid sizing\n" +
                "- Mouse click event handling\n" +
                "- Random wall generation\n" +
                "- Input validation\n\n" +
                "Perfect foundation for maze projects!",
                "About",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }
    }
}