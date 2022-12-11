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
            int rounds = 20;

            for (int r = 0; r < rounds; r++)
            {
                foreach (var monkey in monkeyList)
                {
                    List<int> itemToRemoveIndex = new List<int>();

                    for (int i = 0; i < monkey.ItemList.Count; i++)
                    {
                        int newWorryLevel = monkey.DoOperation(monkey.ItemList[i]);
                        newWorryLevel /= 3;

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
                ItemList = new string(lines[1].Skip(18).ToArray()).Split(", ").Select(s => int.Parse(s)).ToList(),
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
            ItemList = new List<int>();
        }

        public int Number { get; set; }

        public List<int> ItemList { get; set; }

        public string Operation { get; set; }

        public int DivisibleBy { get; set; }

        public int MonkeyNumberIfDivisibleTrue { get; set; }

        public int MonkeyNumberIfDivisibleFalse { get; set; }

        public int InspectionCount { get; set; }

        public int DoOperation(int worryLevel)
        {
            return (int)new NCalc.Expression(Operation.Replace("old", worryLevel.ToString())).Evaluate();
        }
    }
}
