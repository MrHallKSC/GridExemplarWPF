# Grid Exemplar WPF - A-Level Computer Science NEA Foundation

## Overview

This project is an exemplar WPF application designed for AQA 7517 A-Level Computer Science students working on their Non-Exam Assessment (NEA) projects, particularly those creating maze-based or grid-based applications.

The application demonstrates how to:
- Create a grid of squares on a WPF canvas
- Draw shapes using individual lines (walls)
- Handle mouse click events to select squares
- Dynamically resize the grid based on user input
- Implement a menu system and control panel
- Use object-oriented programming principles

This exemplar provides a solid foundation that students can extend for maze generation, pathfinding algorithms, game boards, or similar grid-based projects.

## Project Structure

The project consists of three main files:

1. **Square.cs** - A custom class representing a single grid cell
2. **MainWindow.xaml** - The user interface layout definition
3. **MainWindow.xaml.cs** - The code-behind containing the application logic

## AQA 7517 Specification Skills Demonstrated

This exemplar demonstrates skills from the AQA 7517 specification, particularly from Group A (Programming Fundamentals) and Group B (Data Structures and Algorithms).

### Group A Skills - Programming Fundamentals

| Skill | Implementation in Project |
|-------|---------------------------|
| **Variables and Constants** | `SQUARE_SIZE` constant, `gridRows`/`gridCols` variables, `showGridLines` boolean flag |
| **Data Types** | Integer (`int`), Boolean (`bool`), Double (`double`), String (for TextBox input) |
| **Operators** | Arithmetic (`*`, `+`), Comparison (`>=`, `<`, `==`), Logical (`&&`), Boolean NOT (`!`) |
| **Conditional Statements** | `if` statements for input validation, wall drawing, and click detection |
| **Iteration** | Nested `for` loops to traverse 2D array, iterate through grid rows and columns |
| **String Manipulation** | `ToString()` conversion, `TryParse()` for input validation |
| **Input/Output** | TextBox input, TextBlock output, MessageBox dialogs |
| **Event-Driven Programming** | Button clicks, menu item clicks, mouse click events |

### Group B Skills - Data Structures and Algorithms

| Skill | Implementation in Project |
|-------|---------------------------|
| **Object-Oriented Programming** | `Square` class with properties, methods, and encapsulation |
| **Classes and Objects** | Square class instantiation, object creation in 2D array |
| **Properties** | `X`, `Y`, `Size`, `IsSelected`, `NorthWall`, `EastWall`, `SouthWall`, `WestWall` |
| **Methods** | `ContainsPoint()`, `RandomiseWalls()`, `DrawSquare()`, `InitialiseGrid()` |
| **Constructors** | `Square(int x, int y, int size)` constructor with parameters |
| **2D Arrays** | `Square[,] grid` to store grid cells in rows and columns |
| **Linear Search** | Searching through 2D array to find clicked square |
| **Random Number Generation** | `Random` class to randomly select which walls to draw |
| **Encapsulation** | Square class encapsulates its own data and behavior |

### Additional Programming Concepts

| Concept | Implementation |
|---------|----------------|
| **Input Validation** | Using `TryParse()` to safely convert text input to integers, range checking (1-50) |
| **State Management** | Tracking selected squares, wall configurations, grid dimensions |
| **Coordinate Systems** | Converting grid positions (row, col) to pixel coordinates (x, y) |
| **Event Handlers** | Multiple event handlers responding to user interactions |
| **Code Reuse** | Same method called from both button and menu item |
| **Separation of Concerns** | UI (XAML) separated from logic (code-behind) |

## WPF Concepts Explained

### What is WPF?

**Windows Presentation Foundation (WPF)** is a framework for creating desktop applications in C#. It provides a modern approach to building user interfaces with:
- **Separation of UI and Logic**: The visual design (XAML) is separate from the code (C#)
- **Rich Graphics**: Built-in support for 2D graphics, animations, and styling
- **Event-Driven Model**: The program responds to user actions (clicks, typing, etc.)

### XAML (eXtensible Application Markup Language)

XAML is an XML-based language used to define the user interface. Think of it like HTML for desktop applications.
```xml
<Button Content="Click Me" Click="MyButton_Click"/>
```

This creates a button that displays "Click Me" and calls the `MyButton_Click` method when clicked.

### Code-Behind

The **code-behind** file (`.xaml.cs`) contains the C# code that makes the application work. This is where you:
- Handle events (button clicks, mouse movements)
- Process data
- Update the user interface
- Implement your algorithms

### Key WPF Controls Used in This Project

#### 1. Canvas
```xml
<Canvas x:Name="DrawingCanvas" Background="White">
```
- A drawing surface where you can place elements at specific (x, y) coordinates
- Perfect for games, diagrams, and custom graphics
- Unlike Grid or StackPanel, Canvas gives you pixel-perfect positioning

#### 2. DockPanel
```xml
<DockPanel LastChildFill="True">
```
- Arranges child controls by "docking" them to edges (Top, Bottom, Left, Right)
- Used to create typical application layouts (menu at top, controls on side, main area in center)
- `LastChildFill="True"` means the last child fills all remaining space

#### 3. StackPanel
```xml
<StackPanel Orientation="Vertical">
```
- Stacks controls vertically or horizontally
- Perfect for lists of buttons or form controls
- Automatically handles spacing and layout

#### 4. Menu and MenuItem
```xml
<Menu>
    <MenuItem Header="_File">
        <MenuItem Header="_Exit" Click="Exit_Click"/>
    </MenuItem>
</Menu>
```
- Creates a menu bar like in professional applications
- Underscore (`_`) creates keyboard shortcuts (Alt+F)
- MenuItem can contain other MenuItems for submenus

#### 5. Button
```xml
<Button Content="Apply" Click="ApplyButton_Click"/>
```
- Standard clickable button
- `Content` is what's displayed on the button
- `Click` event links to a method in code-behind

#### 6. TextBox
```xml
<TextBox x:Name="WidthTextBox" Text="10"/>
```
- Allows user to type input
- `x:Name` lets you reference it in code: `WidthTextBox.Text`
- Used for getting user input like numbers or text

#### 7. TextBlock
```xml
<TextBlock Text="Selected: 0"/>
```
- Displays text (read-only for user)
- Can be updated from code to show information
- Lighter weight than Label for simple text display

### Drawing Graphics in WPF

This project draws grid squares using **Line** and **Rectangle** objects:
```csharp
Line line = new Line
{
    X1 = 0,      // Start X coordinate
    Y1 = 0,      // Start Y coordinate
    X2 = 100,    // End X coordinate
    Y2 = 0,      // End Y coordinate
    Stroke = Brushes.Black,
    StrokeThickness = 2
};
Canvas.SetLeft(line, 0);
Canvas.SetTop(line, 0);
DrawingCanvas.Children.Add(line);
```

**Why draw individual lines instead of using a Rectangle border?**
- In maze projects, you need to remove individual walls
- Having separate North, East, South, West walls gives you precise control
- You can change each wall independently (e.g., remove the east wall to connect two cells)

### Event-Driven Programming

WPF applications are **event-driven** - they wait for events (user actions) and respond:
```csharp
private void Button_Click(object sender, RoutedEventArgs e)
{
    // This code runs when the button is clicked
}
```

**Key Event Types:**
- `Click` - User clicks a button or menu item
- `MouseLeftButtonDown` - User presses left mouse button
- `TextChanged` - Text in a TextBox changes
- `Loaded` - Control has finished loading

### Layout and Positioning

**Canvas Positioning:**
```csharp
Canvas.SetLeft(element, 100);  // X coordinate
Canvas.SetTop(element, 50);    // Y coordinate
```

**Why use Canvas for this project?**
- Need exact pixel coordinates for grid squares
- Drawing lines requires precise start and end points
- Allows complete control over where elements appear

## Core Concepts Explained

### 1. The Square Class

The `Square` class is a custom data type that represents one cell in the grid:
```csharp
public class Square
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Size { get; set; }
    public bool IsSelected { get; set; }
    public bool NorthWall { get; set; }
    public bool EastWall { get; set; }
    public bool SouthWall { get; set; }
    public bool WestWall { get; set; }
}
```

**Why create a class?**
- Encapsulates all data about a square in one place
- Makes code easier to understand: `square.IsSelected` is clearer than `selectedArray[row, col]`
- Can add methods that operate on the square's data (like `ContainsPoint()`)
- Follows object-oriented programming principles

**Properties vs Fields:**
- Properties use `{ get; set; }` which provides controlled access to data
- Can add validation in the future without changing external code
- Standard C# convention for public data members

### 2. 2D Arrays for Grid Storage
```csharp
private Square[,] grid;
grid = new Square[gridRows, gridCols];
```

A 2D array naturally represents a grid structure:
- First dimension = rows (vertical)
- Second dimension = columns (horizontal)
- Access with `grid[row, col]`

**Example:** For a 3x3 grid:
```
grid[0,0]  grid[0,1]  grid[0,2]
grid[1,0]  grid[1,1]  grid[1,2]
grid[2,0]  grid[2,1]  grid[2,2]
```

**Why use a 2D array instead of two separate 1D arrays or a list?**
- Mirrors the actual grid structure
- Easy to access neighbors (e.g., `grid[row-1, col]` is the cell above)
- Simple to iterate through rows and columns with nested loops
- Essential for maze algorithms that need to check adjacent cells

### 3. Coordinate System Conversion

The grid has two coordinate systems:

**Grid Coordinates** (which cell):
- Row and column numbers
- Example: `grid[2, 3]` is row 2, column 3

**Pixel Coordinates** (where to draw):
- Actual screen position in pixels
- Example: `x = 120, y = 80`

**Conversion:**
```csharp
int x = col * SQUARE_SIZE;  // Column 3 × 40 = 120 pixels from left
int y = row * SQUARE_SIZE;  // Row 2 × 40 = 80 pixels from top
```

This conversion is crucial for:
- Drawing squares at the correct screen position
- Determining which square was clicked based on mouse coordinates

### 4. Wall-Based Drawing

Each square has four boolean properties for walls:
- `NorthWall` (top)
- `EastWall` (right)
- `SouthWall` (bottom)
- `WestWall` (left)

**Why this approach?**
- Maze algorithms work by removing walls between cells
- Can draw/hide individual walls as needed
- More flexible than drawing complete rectangles
- Students can implement maze generation by systematically removing walls

**Drawing a wall:**
```csharp
if (square.NorthWall)
{
    Line northLine = new Line
    {
        X1 = square.X,              // Top-left corner
        Y1 = square.Y,
        X2 = square.X + square.Size, // Top-right corner
        Y2 = square.Y,
        Stroke = Brushes.Black,
        StrokeThickness = 2
    };
    DrawingCanvas.Children.Add(northLine);
}
```

### 5. Event Handling and User Interaction

**Mouse Click Detection:**
```csharp
private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
{
    Point clickPoint = e.GetPosition(DrawingCanvas);
    // Find which square contains this point
}
```

**The process:**
1. User clicks on canvas
2. Event handler is called automatically
3. Get click coordinates relative to canvas
4. Search through grid to find which square contains that point
5. Update the square's state
6. Redraw to show changes

**Point-in-Rectangle Check:**
```csharp
public bool ContainsPoint(double pointX, double pointY)
{
    return pointX >= X && pointX < X + Size &&
           pointY >= Y && pointY < Y + Size;
}
```
This checks if a point is within the square's boundaries.

### 6. Input Validation

**Converting text to numbers safely:**
```csharp
if (int.TryParse(WidthTextBox.Text, out int newWidth))
{
    // Success - newWidth contains the parsed number
}
else
{
    // Failure - user entered invalid text
}
```

**Why use TryParse instead of Parse?**
- `Parse()` crashes if text isn't a valid number
- `TryParse()` returns `true`/`false` and safely handles invalid input
- Essential for user input validation

**Range Validation:**
```csharp
if (newWidth >= 1 && newWidth <= 50)
{
    // Valid range
}
```
Ensures input is within acceptable limits to prevent:
- Grids that are too small (0 or negative)
- Grids that are too large (performance issues, won't fit on screen)

### 7. Randomization
```csharp
private Random random = new Random();

public void RandomiseWalls(Random random)
{
    NorthWall = random.Next(2) == 1;  // 50% chance
}
```

**Why create Random once as a field?**
- Creating multiple `Random` instances quickly can produce identical sequences
- Reusing one instance ensures better randomness
- More efficient (doesn't recreate the random number generator)

**How `random.Next(2)` works:**
- Returns 0 or 1
- Compare to 1: gives `true` (wall exists) or `false` (no wall)
- Each wall has 50% chance of existing

## Extending This Project for NEA

This exemplar provides a foundation for various NEA projects:

### Maze Generation
Add algorithms to create perfect mazes:
- **Recursive Backtracking**: Start from a cell, randomly remove walls, backtrack when stuck
- **Prim's Algorithm**: Start with all walls, randomly remove walls to connect cells
- **Kruskal's Algorithm**: Treat walls as edges in a graph, remove walls to create minimum spanning tree

Example extension:
```csharp
public void RemoveWall(int row, int col, string direction)
{
    if (direction == "North" && row > 0)
    {
        grid[row, col].NorthWall = false;
        grid[row - 1, col].SouthWall = false;  // Remove matching wall
    }
    // Similar for other directions
}
```

### Pathfinding
Implement algorithms to find routes through the maze:
- **Breadth-First Search (BFS)**: Find shortest path
- **Depth-First Search (DFS)**: Explore all possible paths
- **A* Algorithm**: Efficiently find optimal path using heuristics

### Game Implementation
Create games on the grid:
- Pac-Man style maze game
- Snake game
- Sokoban (box-pushing puzzle)
- Tower defense

### Additional Features Students Could Add

1. **Save/Load Functionality**
   - Save grid to text file
   - Load previously created mazes
   - File I/O demonstrates Group B skills

2. **Start and End Points**
   - Special cell types for maze start/end
   - Different colors or symbols
   - Validates that maze is solvable

3. **Animation**
   - Animate maze generation algorithm step-by-step
   - Show pathfinding algorithm progress
   - Uses WPF DispatcherTimer

4. **Statistics**
   - Count path length
   - Time taken to solve
   - Number of dead ends

5. **Different Cell Types**
   - Walls (impassable)
   - Paths (walkable)
   - Keys/doors
   - Teleporters

## Common Extensions for Student Projects

### Adding a Solve Button
```csharp
private void Solve_Click(object sender, RoutedEventArgs e)
{
    // Implement BFS, DFS, or A* algorithm
    // Mark solution path
    // Animate or highlight the route
}
```

### Saving Grid to File
```csharp
private void SaveGrid()
{
    using (StreamWriter writer = new StreamWriter("maze.txt"))
    {
        writer.WriteLine($"{gridRows},{gridCols}");
        for (int row = 0; row < gridRows; row++)
        {
            for (int col = 0; col < gridCols; col++)
            {
                Square s = grid[row, col];
                writer.WriteLine($"{s.NorthWall},{s.EastWall},{s.SouthWall},{s.WestWall}");
            }
        }
    }
}
```

### Adding Player Movement
```csharp
private int playerRow = 0;
private int playerCol = 0;

private void Window_KeyDown(object sender, KeyEventArgs e)
{
    if (e.Key == Key.Up && !grid[playerRow, playerCol].NorthWall)
    {
        playerRow--;
    }
    // Handle other directions
    DrawGrid();
    DrawPlayer();
}
```

## Key Takeaways for Students

1. **Object-Oriented Design**: Breaking problems into classes (Square) makes code manageable
2. **2D Arrays**: Natural way to represent grid-based data structures
3. **Coordinate Systems**: Converting between logical (row/col) and physical (x/y) coordinates
4. **Event-Driven Programming**: Applications respond to user actions, not linear execution
5. **Input Validation**: Always check user input before using it
6. **Encapsulation**: Classes hide internal details and expose only necessary information
7. **Code Reuse**: Same method can handle multiple events (button and menu item)
8. **Separation of Concerns**: UI (XAML) separated from logic (C#)

## Running the Project

1. Open the solution in Visual Studio 2022 or later
2. Ensure you have .NET Desktop Development workload installed
3. Build and run the project (F5)
4. Click on grid squares to select/deselect them
5. Enter new width/height values and click "Apply Grid Size"
6. Use "Randomise Walls" to generate different patterns
7. Explore the menu options

## Assessment Criteria Alignment

This exemplar addresses key NEA marking criteria:

- **Analysis (Group A/B Skills)**: Demonstrates understanding of required programming skills
- **Design**: Shows clear class structure and method decomposition
- **Technical Solution**: Working implementation of grid system with user interaction
- **Testing**: Includes input validation and error handling
- **Evaluation**: Foundation for complex extensions (maze algorithms, pathfinding)

## Further Reading

- AQA 7517 Specification Section 3.1 (Fundamentals of Programming)
- AQA 7517 Specification Section 3.2 (Fundamentals of Data Structures)
- Microsoft WPF Documentation: https://docs.microsoft.com/en-us/dotnet/desktop/wpf/
- C# Programming Guide: https://docs.microsoft.com/en-us/dotnet/csharp/

## License

This exemplar is provided for educational purposes for AQA 7517 A-Level Computer Science students.

---

**Created for educational use in A-Level Computer Science NEA projects**