namespace AdventOfCode.Utils
{
    internal class Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public Position Diff(Position position)
        {
            return new Position(position.X - X, position.Y - Y);
        }

        public Position DiffAbs(Position position)
        {
            return new Position(Math.Abs(X - position.X), Math.Abs(Y - position.Y));
        }

        public bool IsAdjacent(Position position)
        {
            Position diffPos = DiffAbs(position);

            if (diffPos.X <= 1 && diffPos.Y <= 1)
            {
                return true;
            }

            return false;
        }
    }
}