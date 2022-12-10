using System;
using System.Linq;

namespace Day10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllText(@"input.txt").Split('\n').Where(s => !string.IsNullOrEmpty(s)).ToList();

            int cycle = 1;
            int currentIndexInput = 0;
            int remainingCycle = 0;
            int toAdd = 0;
            int x = 1;

            while (input.Count() > currentIndexInput)
            {
                if (remainingCycle == 0)
                {
                    var instruct = input[currentIndexInput].Split(' ');

                    if (instruct[0] == "noop")
                    {   
                        remainingCycle++;
                    }
                    else if (instruct[0] == "addx")
                    {
                        toAdd = int.Parse(instruct[1]);
                        remainingCycle += 2;
                    }

                    currentIndexInput++;
                }

                remainingCycle--;

                if (remainingCycle == 0)
                {
                    // AddX
                    x += toAdd;
                    toAdd = 0;
                }

                cycle++;

                WriteXAndSignalStrength(cycle, x);
            }
        }

        public static int SumStrength = 0;

        public static void WriteXAndSignalStrength(int cycle, int x)
        {
            if (new int[] { 20, 60, 100, 140, 180, 220 }.Contains(cycle))
            {
                SumStrength += cycle * x;

                Console.WriteLine($"cycle {cycle}, x value {x}, signal strength {cycle * x}");
                Console.WriteLine($"sum strength {SumStrength}");
            }
        }
    }
}
