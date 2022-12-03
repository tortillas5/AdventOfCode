using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace Day3
{
    internal class Program
    {
        public static Dictionary<char, int> Priorities = new Dictionary<char, int>()
        {
            { 'a', 1 },
            { 'b', 2 },
            { 'c', 3 },
            { 'd', 4 },
            { 'e', 5 },
            { 'f', 6 },
            { 'g', 7 },
            { 'h', 8 },
            { 'i', 9 },
            { 'j', 10 },
            { 'k', 11 },
            { 'l', 12 },
            { 'm', 13 },
            { 'n', 14 },
            { 'o', 15 },
            { 'p', 16 },
            { 'q', 17 },
            { 'r', 18 },
            { 's', 19 },
            { 't', 20 },
            { 'u', 21 },
            { 'v', 22 },
            { 'w', 23 },
            { 'x', 24 },
            { 'y', 25 },
            { 'z', 26 },
            { 'A', 27 },
            { 'B', 28 },
            { 'C', 29 },
            { 'D', 30 },
            { 'E', 31 },
            { 'F', 32 },
            { 'G', 33 },
            { 'H', 34 },
            { 'I', 35 },
            { 'J', 36 },
            { 'K', 37 },
            { 'L', 38 },
            { 'M', 39 },
            { 'N', 40 },
            { 'O', 41 },
            { 'P', 42 },
            { 'Q', 43 },
            { 'R', 44 },
            { 'S', 45 },
            { 'T', 46 },
            { 'U', 47 },
            { 'V', 48 },
            { 'W', 49 },
            { 'X', 50 },
            { 'Y', 51 },
            { 'Z', 52 },
        };

        static void Main(string[] args)
        {
            // Part 1
            int prioritiesSum = 0;

            var itemsCompartiments = System.IO.File.ReadAllText(@"input.txt").Split('\n').Select(s => new {
                total = new HashSet<char>(s),
                first = new HashSet<char>(s.Substring(0, s.Length / 2)),
                second = new HashSet<char>(s.Substring(s.Length / 2))
            }).ToList();

            foreach (var itemCompartiment in itemsCompartiments)
            {
                foreach (var item in itemCompartiment.first)
                {
                    if (itemCompartiment.second.Contains(item))
                    {
                        prioritiesSum += Priorities[item];
                    }
                }
            }

            Console.WriteLine(prioritiesSum);

            // Part 2
            int groupPrioritieSum = 0;

            for (int i = 0; i < itemsCompartiments.Count -1; i += 3)
            {
                foreach (var item in itemsCompartiments[i].total)
                {
                    if (itemsCompartiments[i + 1].total.Contains(item) && itemsCompartiments[i + 2].total.Contains(item))
                    {
                        groupPrioritieSum += Priorities[item];
                    }
                }
            }

            Console.WriteLine(groupPrioritieSum);
        }
    }
}
