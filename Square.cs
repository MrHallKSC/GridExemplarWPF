using System;

namespace GridExemplarWPF
{
    /// <summary>
    /// Represents a single square (cell) in the grid, including its location, size, selection state, and walls.
    /// </summary>
    public class Square
    {
        /// <summary>
        /// Gets or sets the X coordinate (pixel position) of the square's top-left corner.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate (pixel position) of the square's top-left corner.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the size of the square, in pixels.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Gets or sets whether this square is currently selected.
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Gets or sets whether the north (top) wall exists.
        /// </summary>
        public bool NorthWall { get; set; }

        /// <summary>
        /// Gets or sets whether the east (right) wall exists.
        /// </summary>
        public bool EastWall { get; set; }

        /// <summary>
        /// Gets or sets whether the south (bottom) wall exists.
        /// </summary>
        public bool SouthWall { get; set; }

        /// <summary>
        /// Gets or sets whether the west (left) wall exists.
        /// </summary>
        public bool WestWall { get; set; }

        /// <summary>
        /// Initializes a new square at a given pixel coordinate with the provided size.
        /// All walls default to present.
        /// </summary>
        /// <param name="x">X coordinate (pixel position).</param>
        /// <param name="y">Y coordinate (pixel position).</param>
        /// <param name="size">Square size (pixels).</param>
        public Square(int x, int y, int size)
        {
            X = x;
            Y = y;
            Size = size;
            IsSelected = false;

            NorthWall = true;
            EastWall = true;
            SouthWall = true;
            WestWall = true;
        }

        /// <summary>
        /// Returns true if the given point lies within this square's bounds.
        /// </summary>
        /// <param name="pointX">X coordinate of the point (relative to the canvas).</param>
        /// <param name="pointY">Y coordinate of the point (relative to the canvas).</param>
        public bool ContainsPoint(double pointX, double pointY)
        {
            return pointX >= X && pointX < X + Size &&
                   pointY >= Y && pointY < Y + Size;
        }

        /// <summary>
        /// Randomly sets each of the four walls to exist or not exist.
        /// </summary>
        /// <param name="random">Random number generator (should be reused, not recreated each call).</param>
        public void RandomiseWalls(Random random)
        {
            NorthWall = random.Next(2) == 1;
            EastWall = random.Next(2) == 1;
            SouthWall = random.Next(2) == 1;
            WestWall = random.Next(2) == 1;
        }
    }
}