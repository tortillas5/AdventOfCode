using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"input.txt").Split('\n')[0];

            Console.WriteLine(GetPosDifferentChar(input, 4)); // 1804
            Console.WriteLine(GetPosDifferentChar(input, 14)); // 2508
        }

        public static int GetPosDifferentChar(string input, int nbChar)
        {
            for (int i = 0; i < input.Length; i++)
            {
                HashSet<char> chars = input.Substring(i, nbChar).ToHashSet<char>();

                if (chars.Count == nbChar)
                {
                    return i + nbChar;
                }
            }

            return -1;
        }
    }
}
