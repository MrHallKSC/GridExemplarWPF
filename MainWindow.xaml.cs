using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        private Random random = new Random();

        // Constructor - runs when the window is created
        public MainWindow()
        {
            InitializeComponent(); // built in method for WPF initialisation
            
            
            InitialiseGrid(); // Set up the grid data structure
            DrawGrid(); // Render the grid on the canvas
            UpdateSelectedCount();  // Initialize the counter display
        }

        // Initialise the 2D array with Square objects
        // Each square knows its own pixel position based on row and column
        private void InitialiseGrid()
        {
            // Create a new 2D array with current dimensions
            // This allows the grid to be resized dynamically
            grid = new Square[gridRows, gridCols];

            // Nested loops iterate through each position in the 2D array
            // Outer loop handles rows, inner loop handles columns
            for (int row = 0; row < gridRows; row++)
            {
                for (int col = 0; col < gridCols; col++)
                {
                    // Calculate pixel coordinates from grid coordinates
                    // Multiply by SQUARE_SIZE to convert grid position to screen position
                    int x = col * SQUARE_SIZE;
                    int y = row * SQUARE_SIZE;

                    // Create a new Square object and store it in the array
                    grid[row, col] = new Square(x, y, SQUARE_SIZE);

                    // Randomise which walls this square has
                    // This creates a random pattern when the grid is initialized
                    grid[row, col].RandomiseWalls(random);
                }
            }
        }

        // Draw all squares on the canvas using individual Line objects
        // This approach shows students how to build complex shapes from primitives
        private void DrawGrid()
        {
            // Clear any existing drawings first
            // This is important if we want to redraw the grid later
            DrawingCanvas.Children.Clear();

            // Draw each square in the grid
            for (int row = 0; row < gridRows; row++)
            {
                for (int col = 0; col < gridCols; col++)
                {
                    Square square = grid[row, col];
                    DrawSquare(square);
                }
            }
        }

        // Draw a single square - only draws the walls that exist
        // This demonstrates conditional rendering based on object properties
        private void DrawSquare(Square square)
        {
            // Choose colour based on whether square is selected
            // Ternary operator: condition ? valueIfTrue : valueIfFalse
            Brush lineColour = square.IsSelected ? Brushes.Red : Brushes.Black;
            Brush fillColour = square.IsSelected ? Brushes.LightCoral : Brushes.White;

            // Draw a filled rectangle as background
            // This makes the selected state visible and fills the cell
            Rectangle background = new Rectangle
            {
                Width = square.Size,
                Height = square.Size,
                Fill = fillColour
            };
            Canvas.SetLeft(background, square.X);
            Canvas.SetTop(background, square.Y);
            DrawingCanvas.Children.Add(background);

            // Only draw walls if showGridLines is true
            // This demonstrates conditional rendering based on program state
            if (showGridLines)
            {
                // Draw North wall (top) - only if it exists
                // This is key for maze projects: walls are drawn individually
                if (square.NorthWall)
                {
                    Line northLine = new Line
                    {
                        X1 = square.X,                  // Start at top-left corner
                        Y1 = square.Y,
                        X2 = square.X + square.Size,    // End at top-right corner
                        Y2 = square.Y,
                        Stroke = lineColour,
                        StrokeThickness = 2
                    };
                    DrawingCanvas.Children.Add(northLine);
                }

                // Draw East wall (right) - only if it exists
                if (square.EastWall)
                {
                    Line eastLine = new Line
                    {
                        X1 = square.X + square.Size,    // Start at top-right corner
                        Y1 = square.Y,
                        X2 = square.X + square.Size,    // End at bottom-right corner
                        Y2 = square.Y + square.Size,
                        Stroke = lineColour,
                        StrokeThickness = 2
                    };
                    DrawingCanvas.Children.Add(eastLine);
                }

                // Draw South wall (bottom) - only if it exists
                if (square.SouthWall)
                {
                    Line southLine = new Line
                    {
                        X1 = square.X + square.Size,    // Start at bottom-right corner
                        Y1 = square.Y + square.Size,
                        X2 = square.X,                  // End at bottom-left corner
                        Y2 = square.Y + square.Size,
                        Stroke = lineColour,
                        StrokeThickness = 2
                    };
                    DrawingCanvas.Children.Add(southLine);
                }

                // Draw West wall (left) - only if it exists
                if (square.WestWall)
                {
                    Line westLine = new Line
                    {
                        X1 = square.X,                  // Start at bottom-left corner
                        Y1 = square.Y + square.Size,
                        X2 = square.X,                  // End at top-left corner
                        Y2 = square.Y,
                        Stroke = lineColour,
                        StrokeThickness = 2
                    };
                    DrawingCanvas.Children.Add(westLine);
                }
            }
        }

        // Event handler for mouse clicks on the canvas
        // This is event-driven programming - responding to user actions
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Get the coordinates where the user clicked
            // e.GetPosition() returns the mouse position relative to the canvas
            Point clickPoint = e.GetPosition(DrawingCanvas);

            // Search through all squares to find which one was clicked
            // This is a linear search through the 2D array
            for (int row = 0; row < gridRows; row++)
            {
                for (int col = 0; col < gridCols; col++)
                {
                    Square square = grid[row, col];

                    // Use the Square's ContainsPoint method to check if click is inside
                    // This demonstrates encapsulation - the Square knows how to check itself
                    if (square.ContainsPoint(clickPoint.X, clickPoint.Y))
                    {
                        // Toggle the selected state (true becomes false, false becomes true)
                        // This is the NOT operator - it flips the boolean value
                        square.IsSelected = !square.IsSelected;

                        // Redraw the entire grid to show the updated state
                        // In a more advanced version, we might only redraw the changed square
                        DrawGrid();

                        // Update the count display to reflect the change
                        UpdateSelectedCount();

                        // Exit the loops once we've found and handled the clicked square
                        // This is more efficient than checking remaining squares
                        return;
                    }
                }
            }
        }

        // Update the display showing how many squares are selected
        // This demonstrates how to keep the UI in sync with program state
        private void UpdateSelectedCount()
        {
            // Counter variable to accumulate the number of selected squares
            int count = 0;

            // Iterate through all squares and count how many are selected
            for (int row = 0; row < gridRows; row++)
            {
                for (int col = 0; col < gridCols; col++)
                {
                    if (grid[row, col].IsSelected)
                    {
                        count++;  // Increment counter for each selected square
                    }
                }
            }

            // Update the TextBlock to display the count
            // .Text property must be a string, so we convert the integer
            SelectedCountLabel.Text = count.ToString();
        }

        // Event handler for Apply Grid Size button
        // This demonstrates input validation and dynamic data structure sizing
        private void ApplyGridSize_Click(object sender, RoutedEventArgs e)
        {
            // Try to parse the text from the textboxes into integers
            // TryParse returns true if successful, false if the text isn't a valid number
            // The 'out' keyword means the parsed value is stored in the variable
            if (int.TryParse(WidthTextBox.Text, out int newWidth) &&
                int.TryParse(HeightTextBox.Text, out int newHeight))
            {
                // Validate the input - grids must be at least 1x1 and not too large
                // Maximum size prevents performance issues and keeps grid visible
                if (newWidth >= 1 && newWidth <= 50 && newHeight >= 1 && newHeight <= 50)
                {
                    // Update the grid dimensions
                    gridCols = newWidth;
                    gridRows = newHeight;

                    // Recreate the grid with new dimensions
                    InitialiseGrid();
                    DrawGrid();
                    UpdateSelectedCount();
                }
                else
                {
                    // Show an error message if values are out of range
                    // MessageBox provides user feedback about invalid input
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
                // Show error message if text couldn't be parsed as integers
                MessageBox.Show(
                    "Please enter valid numbers for width and height.",
                    "Invalid Input",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
            }
        }

        // Event handler for Randomise Walls button
        // This regenerates the wall pattern for all squares
        private void RandomiseWalls_Click(object sender, RoutedEventArgs e)
        {
            // Loop through all squares and randomise their walls
            for (int row = 0; row < gridRows; row++)
            {
                for (int col = 0; col < gridCols; col++)
                {
                    grid[row, col].RandomiseWalls(random);
                }
            }

            // Redraw to show the new wall configuration
            DrawGrid();
        }

        // Event handler for Clear Selection button/menu
        // Both the button and menu item call this same method (code reuse)
        private void ClearSelection_Click(object sender, RoutedEventArgs e)
        {
            // Loop through all squares and set IsSelected to false
            for (int row = 0; row < gridRows; row++)
            {
                for (int col = 0; col < gridCols; col++)
                {
                    grid[row, col].IsSelected = false;
                }
            }

            // Redraw to show the changes and update counter
            DrawGrid();
            UpdateSelectedCount();
        }

        // Event handler for Reset Grid button/menu
        // This re-initializes the entire grid to its starting state
        private void ResetGrid_Click(object sender, RoutedEventArgs e)
        {
            // Re-initialize creates a completely new grid
            // This resets dimensions to textbox values and randomises walls
            InitialiseGrid();
            DrawGrid();
            UpdateSelectedCount();
        }

        // Event handler for Select All button
        // Shows the opposite operation to Clear Selection
        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            // Loop through all squares and set IsSelected to true
            for (int row = 0; row < gridRows; row++)
            {
                for (int col = 0; col < gridCols; col++)
                {
                    grid[row, col].IsSelected = true;
                }
            }

            // Redraw to show the changes and update counter
            DrawGrid();
            UpdateSelectedCount();
        }

        // Event handler for Toggle Grid Lines menu item
        // This demonstrates how to change visual appearance without changing data
        private void ToggleGridLines_Click(object sender, RoutedEventArgs e)
        {
            // Flip the boolean flag using NOT operator
            showGridLines = !showGridLines;

            // Redraw with or without lines depending on new state
            DrawGrid();
        }

        // Event handler for Exit menu item
        // Close() method terminates the application
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            // Close the window, which ends the application
            // In a real application, you might want to check for unsaved changes first
            Close();
        }

        // Event handler for About menu item
        // MessageBox is a simple way to display information to the user
        private void About_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show displays a dialog box with a message
            // It's useful for simple alerts, confirmations, and information
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
                "About",  // Title of the message box
                MessageBoxButton.OK,  // Which buttons to show
                MessageBoxImage.Information  // Icon to display
            );
        }
    }
}