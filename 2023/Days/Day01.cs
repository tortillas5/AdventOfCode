using System.Text.RegularExpressions;
using AdventOfCode.Tools;

namespace AdventOfCode.Days
{
    /// <summary>
    /// Jour 1 de advent of code.
    /// https://adventofcode.com/2023/day/1
    /// </summary>
    internal static class Day01
    {
        /// <summary>
        /// Chemin vers l'input du jour 1.
        /// </summary>
        private static readonly string InputPath = Path.Combine(Environment.CurrentDirectory, "Inputs", "Day01.txt");

        /// <summary>
        /// Compose des nombres sur 2 décimales en prenant le premier et le dernier de chaque ligne, puis les sommes.
        /// </summary>
        /// <returns>Somme de nombres.</returns>
        public static int CalculerPart1()
        {
            List<string> listNombre = [];

            // Pour chaque ligne
            foreach (string line in InputHandler.GetInputLines(InputPath))
            {
                char first;
                char last;
                string nombre;

                // Nombre de nombres dans la ligne
                int nbDigit = line.Count(char.IsDigit);

                // Un seul nombre
                if (nbDigit == 1)
                {
                    first = line.First(char.IsDigit);

                    nombre = char.ToString(first) + char.ToString(first);
                }
                else
                {
                    // Plusieurs nombres

                    first = line.First(char.IsDigit);
                    last = line.Last(char.IsDigit);

                    nombre = char.ToString(first) + char.ToString(last);
                }

                // Ajout du nombre combiné à la liste.
                listNombre.Add(nombre);
            }

            // Passage en int.
            List<int> listeInt = listNombre.Select(int.Parse).ToList();

            return listeInt.Sum();
        }

        /// <summary>
        /// Compose des nombres sur 2 décimales en prenant le premier et le dernier de chaque ligne, qu'il soit sous forme de nombre (0-9) ou de texte (one - nine), puis les sommes.
        /// </summary>
        /// <returns>Somme de nombres.</returns>
        public static int CalculerPart2()
        {
            List<string> inputLines = InputHandler.GetInputLines(InputPath);

            // Regex des valeurs qu'on veut trouver
            string regex = new(@"\d|one|two|three|four|five|six|seven|eight|nine");

            List<string> listNombre = [];

            foreach (string line in inputLines)
            {
                // On regarde le premier match en partant de gauche.
                Match first = Regex.Matches(line, regex)[0];

                // On regarde le premier match en partant de droite.
                Match last = Regex.Matches(line, regex, RegexOptions.RightToLeft)[0];

                // On ajoute les match trouvés.
                listNombre.Add(ToNumber(first.Value).ToString() + ToNumber(last.Value).ToString());
            }

            // Passage en int.
            List<int> listeInt = listNombre.Select(int.Parse).ToList();

            return listeInt.Sum();
        }

        /// <summary>
        /// Retourne une entier à partir d'une chaîne de caractère qui représente un nombre.
        /// </summary>
        /// <param name="nombre">Nombre en texte.</param>
        /// <returns>Un entier.</returns>
        private static int ToNumber(string nombre)
        {
            return nombre switch
            {
                "one" => 1,
                "two" => 2,
                "three" => 3,
                "four" => 4,
                "five" => 5,
                "six" => 6,
                "seven" => 7,
                "eight" => 8,
                "nine" => 9,
                _ => int.Parse(nombre),
            };
        }
    }
}