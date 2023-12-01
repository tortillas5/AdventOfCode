using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Day11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Monkey> monkeyList = System.IO.File.ReadAllText(@"input.txt").Split("\n\n").Select(s => s.Split('\n')).Select(s => MonkeyMaker(s)).ToList();
            int rounds = 10000;
            int modulo = monkeyList.Select(m => m.DivisibleBy).Aggregate(1, (a, b) => a * b);

            for (int r = 1; r <= rounds; r++)
            {
                foreach (var monkey in monkeyList)
                {
                    List<int> itemToRemoveIndex = new List<int>();

                    for (int i = 0; i < monkey.ItemList.Count; i++)
                    {
                        long newWorryLevel = monkey.DoOperation(monkey.ItemList[i]);
                        newWorryLevel %= modulo;

                        if (newWorryLevel % monkey.DivisibleBy == 0)
                        {
                            monkeyList.FirstOrDefault(m => m.Number == monkey.MonkeyNumberIfDivisibleTrue).ItemList.Add(newWorryLevel);
                            itemToRemoveIndex.Add(i);
                        }
                        else
                        {
                            monkeyList.FirstOrDefault(m => m.Number == monkey.MonkeyNumberIfDivisibleFalse).ItemList.Add(newWorryLevel);
                            itemToRemoveIndex.Add(i);
                        }

                        monkey.InspectionCount++;
                    }

                    foreach (var itemToRemove in itemToRemoveIndex.OrderByDescending(d => d))
                    {
                        monkey.ItemList.RemoveAt(itemToRemove);
                    }
                }

                if (new int[] { 1, 20, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000 }.Contains(r))
                {
                    foreach (var monk in monkeyList)
                    {
                        Console.WriteLine($"monk {monk.Number} with {monk.InspectionCount} inspections in {r} rounds");
                    }

                    Console.WriteLine();
                }
            }

            var monkeyInspectors = monkeyList.OrderByDescending(m => m.InspectionCount).Take(2).ToList();

            Console.WriteLine("The best monkey inspectors are :");

            foreach (var monk in monkeyInspectors)
            {
                Console.WriteLine($"monk {monk.Number} with {monk.InspectionCount} inspections in {rounds} rounds");
            }

            Console.WriteLine($"Monkey business level is {monkeyInspectors[0].InspectionCount * monkeyInspectors[1].InspectionCount}");
        }

        public static Monkey MonkeyMaker(string[] lines)
        {
            Monkey monkey = new Monkey
            {
                Number = int.Parse(new string(lines[0].Skip(7).ToArray()).Replace(":", string.Empty)),
                ItemList = new string(lines[1].Skip(18).ToArray()).Split(", ").Select(s => long.Parse(s)).ToList(),
                Operation = new string(lines[2].Skip(19).ToArray()),
                DivisibleBy = int.Parse(new string(lines[3].Skip(21).ToArray())),
                MonkeyNumberIfDivisibleTrue = int.Parse(new string(lines[4].Skip(29).ToArray())),
                MonkeyNumberIfDivisibleFalse = int.Parse(new string(lines[5].Skip(30).ToArray()))
            };

            return monkey;
        }
    }

    public class Monkey
    {
        public Monkey()
        {
            ItemList = new List<long>();
        }

        public int Number { get; set; }

        public List<long> ItemList { get; set; }

        public string Operation { get; set; }

        public int DivisibleBy { get; set; }

        public int MonkeyNumberIfDivisibleTrue { get; set; }

        public int MonkeyNumberIfDivisibleFalse { get; set; }

        public long InspectionCount { get; set; }

        public long DoOperation(long worryLevel)
        {
            string op = Operation.Replace("old", worryLevel.ToString());
            var numbs = op.Contains("+") ? op.Split("+") : op.Split("*");

            return op.Contains("+") ? long.Parse(numbs[0]) + long.Parse(numbs[1]) : long.Parse(numbs[0]) * long.Parse(numbs[1]);
        }
    }
}
