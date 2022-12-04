using System;
using System.Collections.Generic;
using System.Linq;

namespace Day4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var zones = System.IO.File.ReadAllText(@"input.txt")
                .Split('\n') // Take pairs
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.Split(',')) // Take sections
                .Select(s => new
                {
                    first = ZoneEnhancer(s[0]),
                    second = ZoneEnhancer(s[1])
                }).ToList();

            // Part 1
            int fullyContain = 0;

            foreach (var zone in zones)
            {
                if (!zone.second.Except(zone.first).Any() || !zone.first.Except(zone.second).Any())
                {
                    fullyContain++;
                }
            }

            Console.WriteLine(fullyContain);

            // Part 2
            int overLapping = 0;

            foreach (var zone in zones)
            {
                if (zone.first.Intersect(zone.second).Any())
                {
                    overLapping++;
                }
            }

            Console.WriteLine(overLapping);
        }

        // Zone like "99-99" or "12-14"
        // Return 99 and 12, 13, 14
        public static List<string> ZoneEnhancer(string zone)
        {
            var sections = zone.Split('-');
            int begining = int.Parse(sections[0]);
            int end = int.Parse(sections[1]);

            List<string> result = new List<string>();

            for (int i = begining; i <= end; i++)
            {
                result.Add(i.ToString());
            }

            return result;
        }
    }
}
