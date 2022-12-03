using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace Day2
{
    internal class Program
    {
        // A rock B paper C scissors, X rock Y paper Z scissors
        public static Dictionary<string, int> possibleCases = new Dictionary<string, int>()
        {
            { "A X", 3 + 1 },
            { "A Y", 6 + 2 },
            { "A Z", 0 + 3 },
            { "B X", 0 + 1 },
            { "B Y", 3 + 2 },
            { "B Z", 6 + 3 },
            { "C X", 6 + 1 },
            { "C Y", 0 + 2 },
            { "C Z", 3 + 3 }
        };

        // A rock B paper C scissors, X lose Y draw Z win
        public static Dictionary<string, int> possibleScenarios = new Dictionary<string, int>()
        {
            { "A X", 0 + 3 },
            { "A Y", 3 + 1 },
            { "A Z", 6 + 2 },
            { "B X", 0 + 1 },
            { "B Y", 3 + 2 },
            { "B Z", 6 + 3 },
            { "C X", 0 + 2 },
            { "C Y", 3 + 3 },
            { "C Z", 6 + 1 }
        };

        static void Main(string[] args)
        {
            var rounds = System.IO.File.ReadAllText(@"input.txt").Split('\n').Where(s => !string.IsNullOrWhiteSpace(s));

            // Part 1
            int score = rounds.Sum(r => possibleCases[r]);
            Console.WriteLine(score);

            // Part 2
            int score2 = rounds.Sum(r => possibleScenarios[r]);
            Console.WriteLine(score2);
        }
    }
}
