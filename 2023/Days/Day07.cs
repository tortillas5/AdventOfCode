using AdventOfCode.Utils;

namespace AdventOfCode.Days
{
    /// <summary>
    /// Day 7 of the advent of code.
    /// https://adventofcode.com/2023/day/7
    /// </summary>
    internal class Day07
    {
        /// <summary>
        /// Path of the day 7 input.
        /// </summary>
        private static readonly string InputPath = Path.Combine(Environment.CurrentDirectory, "Inputs", "Day07.txt");

        /// <summary>
        /// Enumeration of card values.
        /// </summary>
        internal enum Card
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            T = 10,
            J = 11,
            Q = 12,
            K = 13,
            A = 14
        }

        /// <summary>
        /// Type of possible hands.
        /// </summary>
        internal enum HandType
        {
            FiveOfAKind = 7,
            FourOfAKind = 6,
            FullHouse = 5,
            ThreeOfAKind = 4,
            TwoPair = 3,
            Pair = 2,
            HighCard = 1
        }

        /// <summary>
        /// List of hands played during the game.
        /// </summary>
        public static List<Hand> Hands { get; set; } = [];

        /// <summary>
        /// Sort the hands by rank then return the sum of the total bid * rank for each hand.
        /// </summary>
        /// <returns>Sum of the total bid * rank for each hand.</returns>
        public static long CalculatePart1()
        {
            LoadHands();

            Hands.Sort(Hand.Compare);

            long totalWinning = 0;

            for (int i = 0; i < Hands.Count; i++)
            {
                totalWinning += Hands[i].Bid * (i + 1);
            }

            return totalWinning;
        }

        /// <summary>
        /// Return a card for a char.
        /// </summary>
        /// <param name="card">Char to convert into a card.</param>
        /// <returns>A card.</returns>
        /// <exception cref="NotImplementedException">If the char is not convertible.</exception>
        private static Card GetCard(char card)
        {
            return card switch
            {
                '2' => Card.Two,
                '3' => Card.Three,
                '4' => Card.Four,
                '5' => Card.Five,
                '6' => Card.Six,
                '7' => Card.Seven,
                '8' => Card.Eight,
                '9' => Card.Nine,
                'T' => Card.T,
                'J' => Card.J,
                'Q' => Card.Q,
                'K' => Card.K,
                'A' => Card.A,
                _ => throw new NotImplementedException("This card is not handled."),
            };
        }

        /// <summary>
        /// Load the input into <see cref="Hands"/>.
        /// </summary>
        private static void LoadHands()
        {
            foreach (var line in InputHandler.GetInputLines(InputPath).Where(s => !string.IsNullOrWhiteSpace(s)))
            {
                var cardsAndBid = line.Split(' ');

                Hands.Add(new Hand()
                {
                    Cards = cardsAndBid[0].Select(GetCard).ToArray(),
                    Bid = int.Parse(cardsAndBid[1])
                });
            }
        }

        /// <summary>
        /// Hand of a player during the game.
        /// </summary>
        internal class Hand
        {
            /// <summary>
            /// The bid played with the hand.
            /// </summary>
            public int Bid { get; set; }

            /// <summary>
            /// Cards of the hand (should be 5).
            /// </summary>
            public Card[] Cards { get; set; } = [];

            /// <summary>
            /// Compare a hand to another.
            /// </summary>
            /// <param name="hand1">First hand.</param>
            /// <param name="hand2">Second hand.</param>
            /// <returns>Number superior to 0 if hand1 is greater than hand2, inferior to 0 if it's lesser, and equal to 0 if they are of equal value.</returns>
            public static int Compare(Hand hand1, Hand hand2)
            {
                if (hand1.GetHandType() != hand2.GetHandType())
                {
                    return hand1.GetHandType() - hand2.GetHandType();
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (hand1.Cards[i] != hand2.Cards[i])
                        {
                            return hand1.Cards[i] - hand2.Cards[i];
                        }
                    }
                }

                return 0;
            }

            /// <summary>
            /// Return the type of the hand, <see cref="HandType"/>.
            /// </summary>
            /// <returns>Type of the Hand.</returns>
            /// <exception cref="InvalidOperationException">If the hand don't contain 5 cards.</exception>
            public HandType GetHandType()
            {
                if (Cards.Length != 5)
                {
                    throw new InvalidOperationException("Hand should contain 5 cards.");
                }

                var groupedCards = Cards.GroupBy(c => c, (c, gc) => new { Card = c, Number = gc.Count() });

                if (groupedCards.Any(gc => gc.Number == 5))
                {
                    return HandType.FiveOfAKind;
                }

                if (groupedCards.Any(gc => gc.Number == 4))
                {
                    return HandType.FourOfAKind;
                }

                if (groupedCards.Any(gc => gc.Number == 3) && groupedCards.Any(gc => gc.Number == 2))
                {
                    return HandType.FullHouse;
                }

                if (groupedCards.Any(gc => gc.Number == 3))
                {
                    return HandType.ThreeOfAKind;
                }

                if (groupedCards.Any(gc => gc.Number == 2 && groupedCards.Where(gc2 => gc2.Card != gc.Card).Any(gc2 => gc2.Number == 2)))
                {
                    return HandType.TwoPair;
                }

                if (groupedCards.Any(gc => gc.Number == 2))
                {
                    return HandType.Pair;
                }

                return HandType.HighCard;
            }
        }
    }
}