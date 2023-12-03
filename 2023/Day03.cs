using System.Text.RegularExpressions;
using AdventOfCode.Utils;

namespace AdventOfCode
{
    #region Classes

    internal class EnginePart
    {
        public EnginePart(int number, List<Position> numberPositions)
        {
            Number = number;
            NumberPositions = numberPositions;
        }

        public int Number { get; set; }

        public List<Position> NumberPositions { get; set; }
    }

    internal class SpecialCharacter
    {
        public SpecialCharacter(char character, Position position)
        {
            Character = character;
            Position = position;
        }

        public char Character { get; set; }

        public Position Position { get; set; }
    }

    #endregion Classes

    internal static class Day03
    {
        private static readonly string InputPath = Path.Combine(Environment.CurrentDirectory, "Inputs", "Day03.txt");

        private static readonly char[] SpecialCharacters = ['#', '$', '%', '&', '*', '+', '-', '/', '=', '@'];

        private static List<EnginePart>? EngineParts { get; set; }
        private static List<SpecialCharacter>? SpecialCharactersInFile { get; set; }

        public static int CalculerPart1()
        {
            LoadEngineParts();

            List<int> partsInEngine = new();

            foreach (EnginePart enginePart in EngineParts)
            {
                foreach (SpecialCharacter specialCharacter in SpecialCharactersInFile)
                {
                    if (enginePart.NumberPositions.Exists(specialCharacter.Position.IsAdjacent))
                    {
                        partsInEngine.Add(enginePart.Number);
                    }
                }
            }

            return partsInEngine.Sum();
        }

        private static void LoadEngineParts()
        {
            EngineParts = new List<EnginePart>();
            SpecialCharactersInFile = new List<SpecialCharacter>();

            List<string> lines = InputHandler.GetInputLines(InputPath);

            string regex = new(@"\d+");

            for (int i = 0; i < lines.Count; i++)
            {
                MatchCollection matches = Regex.Matches(lines[i], regex);

                // Numbers.
                foreach (Match match in matches.Cast<Match>())
                {
                    List<Position> numberPositions = new();

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
}