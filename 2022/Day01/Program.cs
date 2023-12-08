using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace Day2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllText(@"input.txt").Split("\r\n").ToList();

            List<Elve> elfList = new List<Elve>();
            int i = 0;
            int noElf = 1;

            foreach (var line in lines)
            {
                if (i == 0)
                {
                    elfList.Add(new Elve(noElf));
                    elfList[noElf - 1].AddCalories(int.Parse(line));
                    i += 1;
                }
                else
                {
                    if (line == "")
                    {
                        noElf += 1;
                        elfList.Add(new Elve(noElf));
                    }
                    else
                    {
                        elfList[noElf - 1].AddCalories(int.Parse(line));
                    }
                }
            }

            // elf avec le + de calories et son numéro.
            var maxVal = elfList.Max(e => e.TotalCalories);
            Console.WriteLine("max cals = " + maxVal);

            var indexMaxVal = elfList.First(e => e.TotalCalories == maxVal);
            Console.WriteLine("num elf max = " + indexMaxVal.ElfNo);

            var orderedList = elfList.OrderByDescending(e => e.TotalCalories).Take(3);

            Console.WriteLine("top 3");

            foreach (var elf in orderedList)
            {
                Console.WriteLine("No elf : " + elf.ElfNo);
                Console.WriteLine("Nb cals : " + elf.TotalCalories);
            }

            var totalTopTroisCal = orderedList.Sum(ol => ol.TotalCalories);

            Console.WriteLine("total cal top 3 : " + totalTopTroisCal);
        }

        internal class Elve
        {
            public int ElfNo = -1;
            public int TotalCalories = 0;

            public Elve(int elfNo)
            {
                ElfNo = elfNo;
            }

            public void AddCalories(int cals)
            {
                TotalCalories += cals;
            }
        }
    }
}