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
        /// List of tiles representing the map.
        /// </summary>
        public static List<Tile> Map { get; set; } = [];

        /// <summary>
        /// List of type of pipes.
        /// </summary>
        public static char[] Pipes { get; set; } = ['|', '-', 'L', 'J', '7', 'F', '.', 'S'];

        public static long CalculatePart1()
        {
            Load();

            Tile startingTile = Map.First(m => m.Name == 'S');

            var adjacentToStartTiles = Map.Where(m => m.Position.IsNextTo(startingTile.Position));

            Tile nextTile = adjacentToStartTiles.First(startingTile.IsConnected);
            Tile previousTile = adjacentToStartTiles.Last(startingTile.IsConnected);

            Tile oldNextTile = startingTile;
            Tile oldPreviousTile = startingTile;

            int steps = 1;

            while (!nextTile.Position.Equals(previousTile.Position))
            {
                Tile ont = oldNextTile;
                Tile opt = oldPreviousTile;

                oldNextTile = nextTile;
                oldPreviousTile = previousTile;

                nextTile = Map.Where(m => m.Position.IsNextTo(nextTile.Position)).First(adj => adj.IsConnected(nextTile) && !adj.Position.Equals(ont.Position));
                previousTile = Map.Where(m => m.Position.IsNextTo(previousTile.Position)).First(adj => adj.IsConnected(previousTile) && !adj.Position.Equals(opt.Position));

                Console.WriteLine($"next : {nextTile.Position}");

                steps++;
            }

            // Parcourir le Loop des deux côtés en même temps.
            // Quand les deux sont au même point, c'est l'endroit le plus loin.
            // On retourne ce point.

            return steps;
        }

        public static long CalculatePart2()
        {
            Load();

            return 2;
        }

        private static void Load()
        {
            Map = [];

            var lines = InputHandler.GetInputLines(InputPath);

            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    Map.Add(new Tile(lines[i][j], new Position(i, j)));
                }
            }
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
                        if ("-LF".Contains(tile.Name))
                        {
                            return true;
                        }

                        return false;

                    case PositionNextToPosition.Right:
                        if ("-J7".Contains(tile.Name))
                        {
                            return true;
                        }

                        return false;

                    case PositionNextToPosition.Top:
                        if ("|7F".Contains(tile.Name))
                        {
                            return true;
                        }

                        return false;

                    case PositionNextToPosition.Bottom:
                        if ("|LJ".Contains(tile.Name))
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