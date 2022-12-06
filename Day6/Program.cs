using System;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"input.txt").Split('\n')[0];

            Console.WriteLine(GetPosDifferentChar(input, 4));
            Console.WriteLine(GetPosDifferentChar(input, 14));
        }

        public static int GetPosDifferentChar(string input, int nbChar)
        {
            for (int i = 0; i < input.Length; i++)
            {
                string chars = input.Substring(i, nbChar);

                foreach (var c in chars)
                {
                    if (chars.Count(m => m == c) > 1)
                    {
                        break;
                    }
                    else if (chars.Last() == c)
                    {
                        return i + nbChar;
                    }
                }
            }

            return -1;
        }
    }
}
