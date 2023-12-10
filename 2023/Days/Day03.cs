using System.Text.RegularExpressions;
using AdventOfCode.Tools;

namespace AdventOfCode.Days
{
    /// <summary>
    /// Jour 3 de advent of code.
    /// https://adventofcode.com/2023/day/3
    /// </summary>
    internal static class Day03
    {
        /// <summary>
        /// Chemin vers l'input du jour 3.
        /// </summary>
        private static readonly string InputPath = Path.Combine(Environment.CurrentDirectory, "Inputs", "Day03.txt");

        /// <summary>
        /// Liste de caractères spéciaux.
        /// </summary>
        private static readonly char[] SpecialCharacters = ['#', '$', '%', '&', '*', '+', '-', '/', '=', '@'];

        /// <summary>
        /// Liste des pièces du moteur.
        /// </summary>
        private static List<EnginePart> EngineParts { get; set; } = [];

        /// <summary>
        /// Liste des caractères spéciaux du fichier.
        /// </summary>
        private static List<SpecialCharacter> SpecialCharactersInFile { get; set; } = [];

        /// <summary>
        /// Récupère les pièces de moteur qui sont à côté d'un caractère spécial et retourne la somme de ces pièces.
        /// </summary>
        /// <returns>Sommes des pièces qui ont un caractère spécial adjacent.</returns>
        public static int CalculerPart1()
        {
            LoadEngineParts();

            List<int> partsInEngine = [];

            foreach (EnginePart enginePart in EngineParts)
            {
                partsInEngine.AddRange(SpecialCharactersInFile.Where(specialCharacter => enginePart.NumberPositions.Exists(specialCharacter.Position.IsAdjacent)).Select(specialCharacter => enginePart.Number));
            }

            return partsInEngine.Sum();
        }

        /// <summary>
        /// Récupère les pièces de moteur qui sont à côté d'un engrenage, puis, s'il n'y a que deux pièces adjacentes à cet engrenage, on calcul le ratio de ces deux pièces.
        /// On retourne ensuite la somme des ratios calculés.
        /// </summary>
        /// <returns>Somme des ratios de pièces moteur.</returns>
        public static int CalculerPart2()
        {
            LoadEngineParts();

            List<SpecialCharacter> gears = SpecialCharactersInFile.Where(sc => sc.Character == '*').ToList();
            List<EnginePart> enginePartsAdjacentToGear = [];

            foreach (EnginePart enginePart in EngineParts)
            {
                enginePartsAdjacentToGear.AddRange(gears.Where(specialCharacter => enginePart.NumberPositions.Exists(specialCharacter.Position.IsAdjacent)).Select(specialCharacter => enginePart));
            }

            List<int> ratios = [];

            foreach (SpecialCharacter gear in gears)
            {
                List<EnginePart> gearedEnginePart = [];
                gearedEnginePart.AddRange(enginePartsAdjacentToGear.Where(enginePart => enginePart.NumberPositions.Exists(gear.Position.IsAdjacent)));

                if (gearedEnginePart.Count == 2)
                {
                    ratios.Add(gearedEnginePart[0].Number * gearedEnginePart[1].Number);
                }
            }

            return ratios.Sum();
        }

        /// <summary>
        /// Parse l'input et rempli <see cref="EngineParts"/> & <see cref="SpecialCharactersInFile"/>.
        /// </summary>
        private static void LoadEngineParts()
        {
            EngineParts = [];
            SpecialCharactersInFile = [];

            List<string> lines = InputHandler.GetInputLines(InputPath);

            string regex = new(@"\d+");

            for (int i = 0; i < lines.Count; i++)
            {
                MatchCollection matches = Regex.Matches(lines[i], regex);

                // Numbers.
                foreach (Match match in matches.Cast<Match>())
                {
                    List<Position> numberPositions = [];

                    for (int j = 0; j < match.Value.Length; j++)
                    {
                        numberPositions.Add(new Position(i, match.Index + j));
                    }

                    EngineParts.Add(new(int.Parse(match.Value), numberPositions));
                }

                // Special characters.
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (SpecialCharacters.Contains(lines[i][j]))
                    {
                        SpecialCharactersInFile.Add(new SpecialCharacter(lines[i][j], new Position(i, j)));
                    }
                }
            }
        }
    }

    /// <summary>
    /// Une pièce de moteur.
    /// </summary>
    /// <param name="number">Numéro de la pièce de moteur.</param>
    /// <param name="numberPositions">Les positions de la pièce.</param>
    internal class EnginePart(int number, List<Position> numberPositions)
    {
        /// <summary>
        /// Numéro de la pièce de moteur.
        /// </summary>
        public int Number => number;

        /// <summary>
        /// Les positions de la pièce (ex : si c'est un nombre sur 3 décimales, 3 positions).
        /// </summary>
        public List<Position> NumberPositions => numberPositions;
    }

    /// <summary>
    /// Un caractère spécial avec une position.
    /// </summary>
    /// <param name="character">Caractère spécial.</param>
    /// <param name="position">Position du caractère.</param>
    internal class SpecialCharacter(char character, Position position)
    {
        /// <summary>
        /// Un caractère spécial.
        /// </summary>
        public char Character => character;

        /// <summary>
        /// Position du caractère spécial.
        /// </summary>
        public Position Position => position;
    }
}