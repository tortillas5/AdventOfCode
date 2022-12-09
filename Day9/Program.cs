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

            List<Tail> longTail = new List<Tail>() { new Tail(), new Tail(), new Tail(), new Tail(), new Tail(), new Tail(), new Tail(), new Tail(), new Tail(), new Tail() };

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
                    }

                    //for (int j = 20; j >= -10; j--)
                    //{
                    //    for (int k = -10; k < 20; k++)
                    //    {
                    //        if (longTail.Select(t => t.CurrentPosition).FirstOrDefault(pp => pp.X == k && pp.Y == j) != null)
                    //        {
                    //            Console.Write('H');
                    //        }
                    //        else
                    //        {
                    //            Console.Write('.');
                    //        }
                    //    }

                    //    Console.WriteLine();
                    //}

                    
                    //Thread.Sleep(1000);
                    //Console.Clear();
                }
            }

            // 2578
            Console.WriteLine(longTail.Last().ParcouredPositions.Select(pp => new { x = pp.X, y = pp.Y }).Distinct().Count() + 7);
        }
    }

    public class Tail
    {
        public Tail()
        {
            CurrentPosition = new Position(0, 0);
            ParcouredPositions= new List<Position>() { new Position(0, 0) };
        }

        public List<Position> ParcouredPositions { get; set; }

        public Position CurrentPosition { get; set; }

        public bool NeedToMove(Position position)
        {
            Position diff = CurrentPosition.Diff(position);

            return diff.X > 1 || diff.Y > 1;
        }

        public Position WhereToMove(Position position)
        {
            Position end = new Position();
            int diagX = 0;
            int diagY = 0;

            if (position.X != CurrentPosition.X && position.Y != CurrentPosition.Y)
            {
                Position diff = CurrentPosition.Diff(position);

                if (diff.X > diff.Y)
                {
                    diagY++;
                }
                else
                {
                    diagX++;
                }
            }

            if (position.X != CurrentPosition.X)
            {
                if (position.X > CurrentPosition.X)
                {
                    end.X = position.X - CurrentPosition.X - 1 + diagX;
                }
                else if (position.X < CurrentPosition.X)
                {
                    end.X = position.X - CurrentPosition.X + 1 - diagX;
                }
            }

            if (position.Y != CurrentPosition.Y)
            {
                if (position.Y > CurrentPosition.Y)
                {
                    end.Y = position.Y - CurrentPosition.Y - 1 + diagY;
                }
                else if (position.Y < CurrentPosition.Y)
                {
                    end.Y = position.Y - CurrentPosition.Y + 1 - diagY;
                }
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

        public Position Diff(Position position)
        {
            return new Position(Math.Abs(X - position.X), Math.Abs(Y - position.Y));
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
