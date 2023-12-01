using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"input.txt");
            
            var lines = input.Split("\n\n")[0].Split('\n').Select(s => s.Replace("[", string.Empty).Replace("]", string.Empty).Replace("    ", "|").Replace(" ", string.Empty))
                .Select(s => s.ToArray()).Reverse().Skip(1).ToList();

            string[] stacks = new string[lines[0].Length];

            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    var test = stacks[j];
                    stacks[j] += lines[i][j];
                }
            }

            List<string> readyToMove = stacks.Select(s => s.Replace("|", string.Empty)).ToList();

            var moves = input.Split("\n\n")[1].Split("\n").Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Split(" ")).ToList().Select(k => new { qty = int.Parse(k[1]), begining = int.Parse(k[3]) - 1, end = int.Parse(k[5]) - 1 }).ToList();

            foreach (var move in moves)
            {
                string moved = readyToMove[move.begining].Substring(readyToMove[move.begining].Length - move.qty, move.qty);
                readyToMove[move.begining] = readyToMove[move.begining].Substring(0, readyToMove[move.begining].Length - move.qty);
                readyToMove[move.end] += new string(moved.Reverse().ToArray());
            }

            // Part 1
            foreach (var stack in readyToMove)
            {
                Console.Write(stack.Last());
            }

            Console.WriteLine();

            // Part 2
            List<string> readyToMove2 = stacks.Select(s => s.Replace("|", string.Empty)).ToList();

            foreach (var move in moves)
            {
                string moved = readyToMove2[move.begining].Substring(readyToMove2[move.begining].Length - move.qty, move.qty);
                readyToMove2[move.begining] = readyToMove2[move.begining].Substring(0, readyToMove2[move.begining].Length - move.qty);
                readyToMove2[move.end] += moved;
            }
            
            foreach (var stack in readyToMove2)
            {
                Console.Write(stack.Last());
            }

            Console.WriteLine();
        }
    }
}
