using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridExemplarWPF
{
    // Represents a single square in the grid
    public class Square
    {
        // Properties to store the square's position and state
        public int X { get; set; }          // X coordinate (grid position in pixels)
        public int Y { get; set; }          // Y coordinate (grid position in pixels)
        public int Size { get; set; }       // Size of the square in pixels
        public bool IsSelected { get; set; } // Whether the square is currently selected

        // Wall properties - each square can have up to 4 walls
        // Using NESW (North/East/South/West) is standard for maze algorithms
        // true = wall exists, false = wall is open/removed
        public bool NorthWall { get; set; }  // Top wall
        public bool EastWall { get; set; }   // Right wall
        public bool SouthWall { get; set; }  // Bottom wall
        public bool WestWall { get; set; }   // Left wall

        // Constructor to initialise a new square
        // By default, we create all four walls (a closed cell)
        public Square(int x, int y, int size)
        {
            X = x;
            Y = y;
            Size = size;
            IsSelected = false;  // Squares start unselected

            // Initialize all walls to true (all walls present)
            // In a maze generation algorithm, students would remove walls to create paths
            NorthWall = true;
            EastWall = true;
            SouthWall = true;
            WestWall = true;
        }

        // Check if a point (mouse click) is inside this square
        // This uses boundary checking - the point must be within the square's bounds
        public bool ContainsPoint(double pointX, double pointY)
        {
            return pointX >= X && pointX < X + Size &&
                   pointY >= Y && pointY < Y + Size;
        }

        // Randomly set which walls this square should have
        // This demonstrates using Random to make decisions
        // In a real maze, walls would be removed systematically using algorithms
        public void RandomiseWalls(Random random)
        {
            // Each wall has a 50% chance of existing
            // random.Next(2) returns 0 or 1, which we compare to get true/false
            NorthWall = random.Next(2) == 1;
            EastWall = random.Next(2) == 1;
            SouthWall = random.Next(2) == 1;
            WestWall = random.Next(2) == 1;
        }
    }
}