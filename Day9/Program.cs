using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;

namespace Day9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var moves = System.IO.File.ReadAllText(@"input.txt").Split('\n').Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => new Move(s.Split(' ')[0], s.Split(' ')[1])).ToList();

            Position HeadPosition = new Position(0, 0);
            Tail tail = new Tail();

            foreach (var move in moves)
            {
                for (int i = 1; i <= move.Number; i++)
                {
                    switch (move.Direction)
                    {
                        case 'U':
                            HeadPosition.Y++;
                            break;
                        case 'D':
                            HeadPosition.Y--;
                            break;
                        case 'L':
                            HeadPosition.X--;
                            break;
                        case 'R':
                            HeadPosition.X++;
                            break;
                    }

                    if (tail.NeedToMove(HeadPosition))
                    {
                        Position newPosition = tail.WhereToMove(HeadPosition);
                        tail.CurrentPosition.X += newPosition.X;
                        tail.CurrentPosition.Y += newPosition.Y;
                        tail.ParcouredPositions.Add(new Position(tail.CurrentPosition));
                    }
                }
            }

            // P1 6175
            Console.WriteLine(tail.ParcouredPositions.Select(pp => new { x = pp.X, y = pp.Y }).Distinct().Count());

            List<Tail> longTail = new List<Tail>() { new Tail('H'), new Tail('1'), new Tail('2'), new Tail('3'), new Tail('4'), new Tail('5'), new Tail('6'), new Tail('7'), new Tail('8'), new Tail('9') };

            foreach (var move in moves)
            {
                for (int i = 1; i <= move.Number; i++)
                {
                    switch (move.Direction)
                    {
                        case 'U':
                            longTail[0].CurrentPosition.Y++;
                            break;
                        case 'D':
                            longTail[0].CurrentPosition.Y--;
                            break;
                        case 'L':
                            longTail[0].CurrentPosition.X--;
                            break;
                        case 'R':
                            longTail[0].CurrentPosition.X++;
                            break;
                    }

                    for (int j = 0; j < longTail.Count; j++)
                    {
                        if (longTail[j].NeedToMove(longTail[j == 0 ? 0 : j - 1].CurrentPosition))
                        {
                            Position newPosition = longTail[j].WhereToMove(longTail[j == 0 ? 0 : j - 1].CurrentPosition);
                            longTail[j].CurrentPosition.X += newPosition.X;
                            longTail[j].CurrentPosition.Y += newPosition.Y;
                            longTail[j].ParcouredPositions.Add(new Position(longTail[j].CurrentPosition));
                        }

                        //for (int m = 20; m >= -10; m--)
                        //{
                        //    for (int k = -10; k < 20; k++)
                        //    {
                        //        Tail t = longTail.FirstOrDefault(t => t.CurrentPosition.X == k && t.CurrentPosition.Y == m);
                        //        if (t != null)
                        //        {
                        //            Console.Write(t.Index);
                        //        }
                        //        else
                        //        {
                        //            Console.Write('.');
                        //        }
                        //    }

                        //    Console.WriteLine();
                        //}

                        //Thread.Sleep(100);
                        //Console.Clear();
                    }
                }
            }

            // 2578
            Console.WriteLine(longTail.Last().ParcouredPositions.Select(pp => new { x = pp.X, y = pp.Y }).Distinct().Count());
        }
    }

    public class Tail
    {
        public Tail(char? index = null)
        {
            CurrentPosition = new Position(0, 0);
            ParcouredPositions = new List<Position>() { new Position(0, 0) };
            Index = index ?? '0';
        }

        public List<Position> ParcouredPositions { get; set; }

        public Position CurrentPosition { get; set; }

        public char Index { get; set; }

        public bool NeedToMove(Position position)
        {
            Position diff = CurrentPosition.DiffAbs(position);

            return diff.X > 1 || diff.Y > 1;
        }

        public Position WhereToMove(Position position)
        {
            Position end = new Position();

            Position diff = CurrentPosition.Diff(position);

            // > 
            if (diff.X == 2 && diff.Y == 0)
            {
                end.X++;
            }

            // <
            if (diff.X == -2 && diff.Y == 0)
            {
                end.X--;
            }

            // ^
            if (diff.Y == 2 && diff.X == 0)
            {
                end.Y++;
            }

            // v
            if (diff.Y == -2 && diff.X == 0)
            {
                end.Y--;
            }

            // ^>
            if (diff.Y == 2 && diff.X == 1)
            {
                end.X++;
                end.Y++;
            }

            // v>
            if (diff.Y == -2 && diff.X == 1)
            {
                end.X++;
                end.Y--;
            }

            // ^<
            if (diff.Y == 2 && diff.X == -1)
            {
                end.X--;
                end.Y++;
            }

            // v<
            if (diff.Y == -2 && diff.X == -1)
            {
                end.X--;
                end.Y--;
            }

            // >^
            if (diff.X == 2 && diff.Y == 1)
            {
                end.X++;
                end.Y++;
            }

            // >v
            if (diff.X == 2 && diff.Y == -1)
            {
                end.X++;
                end.Y--;
            }

            // <^
            if (diff.X == -2 && diff.Y == 1)
            {
                end.X--;
                end.Y++;
            }

            // <v
            if (diff.X == -2 && diff.Y == -1)
            {
                end.X--;
                end.Y--;
            }

            // >>^^
            if (diff.X == 2 && diff.Y == 2)
            {
                end.X++;
                end.Y++;
            }

            // >>vv
            if (diff.X == 2 && diff.Y == -2)
            {
                end.X++;
                end.Y--;
            }

            // <<vv
            if (diff.X == -2 && diff.Y == -2)
            {
                end.X--;
                end.Y--;
            }

            // <<^^
            if (diff.X == -2 && diff.Y == 2)
            {
                end.X--;
                end.Y++;
            }

            return end;
        }
    }

    public class Position
    {
        public Position() {}

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Position(Position position)
        {
            this.X = position.X;
            this.Y = position.Y;
        }

        public int X;
        public int Y;

        public Position DiffAbs(Position position)
        {
            return new Position(Math.Abs(X - position.X), Math.Abs(Y - position.Y));
        }

        public Position Diff(Position position)
        {
            return new Position(position.X - X, position.Y - Y);
        }
    }

    public class Move
    {
        public Move(string direction, string number)
        {
            this.Direction = char.Parse(direction);
            this.Number = short.Parse(number);
        }

        public char Direction;
        public short Number;
    }
}
