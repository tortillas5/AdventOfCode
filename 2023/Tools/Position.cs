namespace AdventOfCode.Tools
{
    /// <summary>
    /// Represent a position in a X and Y grid.
    /// </summary>
    internal class Position
    {
        /// <summary>
        /// Initialize a new instance of <see cref="Position"/>.
        /// </summary>
        public Position()
        { }

        /// <summary>
        /// Initialize a new instance of <see cref="Position"/>.
        /// </summary>
        /// <param name="x">X coordinate of the position.</param>
        /// <param name="y">Y coordinate of the position.</param>
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// X coordinate of the position.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y coordinate of the position.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Return a position representing the absolute difference of a position and the current one.
        /// </summary>
        /// <param name="position">Position to do the difference on.</param>
        /// <returns>Position representing an absolute difference.</returns>
        public Position AbsoluteDifference(Position position)
        {
            return new Position(Math.Abs(X - position.X), Math.Abs(Y - position.Y));
        }

        /// <summary>
        /// Return a position representing the difference of a position and the current one.
        /// </summary>
        /// <param name="position">Position to do the difference on.</param>
        /// <returns>Position representing a difference.</returns>
        public Position Difference(Position position)
        {
            return new Position(position.X - X, position.Y - Y);
        }

        /// <summary>
        /// Tell if a position is adjacent to the current one.
        /// </summary>
        /// <param name="position">Position to compare.</param>
        /// <returns>Value indicating whether the position is adjacent.</returns>
        public bool IsAdjacent(Position position)
        {
            Position diffPos = AbsoluteDifference(position);

            if (diffPos.X <= 1 && diffPos.Y <= 1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Convert a position into a readable string.
        /// </summary>
        /// <returns>A string representing the position.</returns>
        public override string ToString()
        {
            return $"X {X}, Y {Y}";
        }
    }
}