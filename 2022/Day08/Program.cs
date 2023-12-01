using System;
using System.Collections.Generic;
using System.Linq;

namespace Day8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllText(@"input.txt").Split('\n').Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.ToArray()).ToArray();
            int visibleTree = input.Length * 2 + input[0].Length * 2 - 4;

            for (int y = 1; y < input.Length - 1; y++)
            {
                var currentY = input[y];

                for (int x = 1; x < currentY.Length - 1; x++)
                {
                    // Récupère un arbre
                    short currentX = short.Parse(currentY[x].ToString());

                    bool visibleXavant = true;
                    bool visibleXapres = true;
                    bool visibleYavant = true;
                    bool visibleYapres = true;

                    // x avant
                    for (int i = x - 1; i >= 0; i--)
                    {
                        if (short.Parse(currentY[i].ToString()) >= currentX)
                        {
                            visibleXavant = false;
                        }
                    }

                    // x après
                    for (int i = x + 1; i < input[y].Length; i++)
                    {
                        if (short.Parse(currentY[i].ToString()) >= currentX)
                        {
                            visibleXapres = false;
                        }
                    }

                    // y avant
                    for (int i = y - 1; i >= 0; i--)
                    {
                        if (short.Parse(input[i][x].ToString()) >= currentX)
                        {
                            visibleYavant = false;
                        }
                    }

                    // y après
                    for (int i = y + 1; i < input.Length; i++)
                    {
                        if (short.Parse(input[i][x].ToString()) >= currentX)
                        {
                            visibleYapres = false;
                        }
                    }

                    if (visibleXavant || visibleXapres || visibleYavant || visibleYapres)
                    {
                        visibleTree++;
                    }
                }
            }

            // Part 1
            Console.WriteLine(visibleTree);

            List<int> scoreDarbre = new List<int>();

            for (int y = 0; y < input.Length; y++)
            {
                var currentY = input[y];

                for (int x = 0; x < currentY.Length; x++)
                {
                    // Récupère un arbre
                    short currentX = short.Parse(currentY[x].ToString());
                    int scoreXavant = 0;
                    int scoreXapres = 0;
                    int scoreYavant = 0;
                    int scoreYapres = 0;

                    // x avant
                    for (int i = x - 1; i >= 0; i--)
                    {
                        if (short.Parse(currentY[i].ToString()) < currentX)
                        {
                            scoreXavant = x - i;
                        }
                        else if (short.Parse(currentY[i].ToString()) >= currentX)
                        {
                            scoreXavant = x - i;
                            break;
                        }
                    }

                    // x après
                    for (int i = x + 1; i < input[y].Length; i++)
                    {
                        if (short.Parse(currentY[i].ToString()) < currentX)
                        {
                            scoreXapres = i - x;
                        }
                        else if (short.Parse(currentY[i].ToString()) >= currentX)
                        {
                            scoreXapres = i - x;
                            break;
                        }
                    }

                    // y avant
                    for (int i = y - 1; i >= 0; i--)
                    {
                        if (short.Parse(input[i][x].ToString()) < currentX)
                        {
                            scoreYavant = y - i;
                        }
                        else if (short.Parse(input[i][x].ToString()) >= currentX)
                        {
                            scoreYavant = y - i;
                            break;
                        }
                    }

                    // y après
                    for (int i = y + 1; i < input.Length; i++)
                    {
                        if (short.Parse(input[i][x].ToString()) < currentX)
                        {
                            scoreYapres = i - y;
                        }
                        else if (short.Parse(input[i][x].ToString()) >= currentX)
                        {
                            scoreYapres = i - y;
                            break;
                        }
                    }

                    scoreDarbre.Add(scoreXavant * scoreXapres * scoreYavant * scoreYapres);
                }
            }

            // BigTree part 2
            Console.WriteLine(scoreDarbre.Max());
        }
    }
}
