using AdventOfCode.Utils;

namespace AdventOfCode
{
    #region Utils

    internal enum Color
    {
        red,
        green,
        blue
    }

    internal class Game
    {
        public int GameId { get; set; }
        public List<List<Set>> Sets { get; set; }
    }

    internal class Set
    {
        public Color Color { get; set; }
        public int Number { get; set; }
    }

    #endregion Utils

    internal static class Day02
    {
        private static readonly string InputPath = Path.Combine(Environment.CurrentDirectory, "Inputs", "Day02.txt");

        public static List<Game>? Games { get; set; }

        public static int CalculerPart1()
        {
            LoadGames();

            Set red = new() { Number = 12, Color = Color.red };
            Set green = new() { Number = 13, Color = Color.green };
            Set blue = new() { Number = 14, Color = Color.blue };

            HashSet<int> invalidGameId = new();

            foreach (var game in Games)
            {
                bool isGamePossible = true;

                foreach (var set in game.Sets)
                {
                    var redSet = set.Find(s => s.Color == Color.red);
                    var greenSet = set.Find(s => s.Color == Color.green);
                    var blueSet = set.Find(s => s.Color == Color.blue);

                    isGamePossible = (redSet == null || redSet.Number <= red.Number) && isGamePossible;
                    isGamePossible = (greenSet == null || greenSet.Number <= green.Number) && isGamePossible;
                    isGamePossible = (blueSet == null || blueSet.Number <= blue.Number) && isGamePossible;
                }

                if (!isGamePossible)
                {
                    invalidGameId.Add(game.GameId);
                }
            }

            var validGames = Games.Where(g => !invalidGameId.Contains(g.GameId));

            return validGames.Sum(vg => vg.GameId);
        }

        private static void LoadGames()
        {
            List<string> lines = InputHandler.GetInputLines(InputPath);
            Games = new List<Game>();

            foreach (string line in lines)
            {
                string[] gameAndSets = line.Split(':');
                int gameNumber = int.Parse(gameAndSets[0].Replace("Game ", string.Empty));
                string[] sets = gameAndSets[1].Split(';');

                Game game = new() { GameId = gameNumber, Sets = new List<List<Set>>() };

                foreach (string set in sets)
                {
                    List<Set> setList = new();
                    string[] cubes = set.Split(",");

                    foreach (var cube in cubes)
                    {
                        string[] c = cube.Trim().Split(' ');
                        Set s = new() { Number = int.Parse(c[0]), Color = (Color)Enum.Parse(typeof(Color), c[1]) };

                        setList.Add(s);
                    }

                    game.Sets.Add(setList);
                }

                Games.Add(game);
            }
        }
    }
}