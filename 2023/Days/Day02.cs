using AdventOfCode.Tools;

namespace AdventOfCode.Days
{
    /// <summary>
    /// Jour 2 de advent of code.
    /// https://adventofcode.com/2023/day/2
    /// </summary>
    internal static class Day02
    {
        /// <summary>
        /// Chemin vers l'input du jour 2.
        /// </summary>
        private static readonly string InputPath = Path.Combine(Environment.CurrentDirectory, "Inputs", "Day02.txt");

        /// <summary>
        /// Énumération des couleurs de cubes.
        /// </summary>
        internal enum Color
        {
            red,
            green,
            blue
        }

        /// <summary>
        /// Liste des parties jouées.
        /// </summary>
        public static List<Game> Games { get; set; } = [];

        /// <summary>
        /// Calcul quelles parties sont possibles à jouer pour un nombre de cubes de chaque couleur et retourne la somme de leurs ids.
        /// </summary>
        /// <returns>Somme des ids des parties jouables.</returns>
        public static int CalculerPart1()
        {
            LoadGames();

            Set red = new(Color.red, 12);
            Set green = new(Color.green, 13);
            Set blue = new(Color.blue, 14);

            HashSet<int> invalidGameId = [];

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

        /// <summary>
        /// Calcul le nombre minimum de cubes présents pour chaque partie et calcul leur puissance.
        /// </summary>
        /// <returns>Somme des puissances de cubes.</returns>
        public static int CalculerPart2()
        {
            LoadGames();

            int total = 0;

            foreach (var game in Games)
            {
                var sets = game.Sets.SelectMany(s => s).GroupBy(s => s.Color, (color, listSets) => new Set(color, listSets.Max(se => se.Number))).ToList();

                Set redSet = sets.Find(s => s.Color == Color.red) ?? new Set();
                Set greenSet = sets.Find(s => s.Color == Color.green) ?? new Set();
                Set blueSet = sets.Find(s => s.Color == Color.blue) ?? new Set();

                int power = redSet.Number * greenSet.Number * blueSet.Number;
                total += power;
            }

            return total;
        }

        /// <summary>
        /// Parse l'input et rempli <see cref="Games"/>.
        /// </summary>
        private static void LoadGames()
        {
            List<string> lines = InputHandler.GetInputLines(InputPath);
            Games = [];

            foreach (string line in lines)
            {
                string[] gameAndSets = line.Split(':');
                int gameNumber = int.Parse(gameAndSets[0].Replace("Game ", string.Empty));
                string[] sets = gameAndSets[1].Split(';');

                Game game = new(gameNumber, []);

                foreach (string set in sets)
                {
                    List<Set> setList = [];
                    string[] cubes = set.Split(",");

                    foreach (var cube in cubes)
                    {
                        string[] c = cube.Trim().Split(' ');
                        Set s = new((Color)Enum.Parse(typeof(Color), c[1]), int.Parse(c[0]));

                        setList.Add(s);
                    }

                    game.Sets.Add(setList);
                }

                Games.Add(game);
            }
        }

        /// <summary>
        /// Représente une partie jouée.
        /// </summary>
        internal class Game(int gameId, List<List<Set>> sets)
        {
            /// <summary>
            /// Numéro de la partie.
            /// </summary>
            public int GameId => gameId;

            /// <summary>
            /// Chaque partie possède plusieurs tirages (première liste).
            /// Chaque tirage possède plusieurs sets (deuxième liste).
            /// </summary>
            public List<List<Set>> Sets => sets;
        }

        /// <summary>
        /// Représente un ensemble de cube tirés d'une couleur.
        /// </summary>
        internal class Set
        {
            public Set()
            { }

            public Set(Color color, int number)
            {
                Color = color;
                Number = number;
            }

            /// <summary>
            /// Couleur du cube.
            /// </summary>
            public Color Color { get; set; }

            /// <summary>
            /// Nombre de cubes de cette couleur.
            /// </summary>
            public int Number { get; set; }
        }
    }
}