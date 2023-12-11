using AdventOfCode.Tools;

namespace AdventOfCode.Days
{
    /// <summary>
    /// Day 10 of the advent of code.
    /// https://adventofcode.com/2023/day/10
    /// </summary>
    internal class Day10
    {
        /// <summary>
        /// Path of the day 10 input.
        /// </summary>
        private static readonly string InputPath = Path.Combine(Environment.CurrentDirectory, "Inputs", "Day10.txt");

        /// <summary>
        /// List of type of pipes.
        /// </summary>
        public static char[] Pipes { get; set; } = ['|', '-', 'L', 'J', '7', 'F', '.', 'S'];

        /// <summary>
        /// List of tiles representing the map.
        /// </summary>
        public static Tile[] TileMap { get; set; } = [];

        public static long CalculatePart1()
        {
            Load();

            int steps = 0;
            Tile? currentTile = TileMap.First(m => m.Name == 'S');
            Tile[] previousTiles = [currentTile, currentTile];

            while (true)
            {
                currentTile = Array.Find(TileMap, t => !previousTiles.Contains(t) && currentTile.Position.IsNextTo(t.Position) && currentTile.IsConnected(t));

                if (currentTile == null)
                {
                    break;
                }

                previousTiles[0] = previousTiles[1];
                previousTiles[1] = currentTile;

                steps++;
            }

            return (steps + 1) / 2;
        }

        public static long CalculatePart2()
        {
            Load();

            return -1;
        }

        private static void Load()
        {
            var lines = InputHandler.GetInputLines(InputPath);
            var tileMap = new List<Tile>();

            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    tileMap.Add(new Tile(lines[i][j], new Position(j, i)));
                }
            }

            TileMap = [.. tileMap];
        }

        internal class Tile
        {
            public Tile()
            {
                Position = new Position();
            }

            public Tile(char tile, Position position)
            {
                Name = tile;
                Position = position;
            }

            public char Name { get; set; }

            public Position Position { get; set; }

            public bool IsConnected(Tile tile)
            {
                PositionNextToPosition positionNextToPosition = Position.GetPositionNextToPosition(tile.Position);

                switch (positionNextToPosition)
                {
                    case PositionNextToPosition.Left:
                        if ("-LF".Contains(tile.Name) && !"|LF".Contains(Name))
                        {
                            return true;
                        }

                        return false;

                    case PositionNextToPosition.Right:
                        if ("-J7".Contains(tile.Name) && !"|J7".Contains(Name))
                        {
                            return true;
                        }

                        return false;

                    case PositionNextToPosition.Top:
                        if ("|7F".Contains(tile.Name) && !"-7F".Contains(Name))
                        {
                            return true;
                        }

                        return false;

                    case PositionNextToPosition.Bottom:
                        if ("|LJ".Contains(tile.Name) && !"-LJ".Contains(Name))
                        {
                            return true;
                        }

                        return false;

                    default:
                        throw new NotImplementedException("Cannot determine if the tile is connected.");
                }
            }
        }
    }
}