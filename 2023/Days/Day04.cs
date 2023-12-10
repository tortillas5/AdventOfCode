using AdventOfCode.Tools;

namespace AdventOfCode.Days
{
    /// <summary>
    /// Jour 4 de advent of code.
    /// https://adventofcode.com/2023/day/4
    /// </summary>
    internal class Day04
    {
        /// <summary>
        /// Chemin vers l'input du jour 4.
        /// </summary>
        private static readonly string InputPath = Path.Combine(Environment.CurrentDirectory, "Inputs", "Day04.txt");

        /// <summary>
        /// Liste de cartes à grater.
        /// </summary>
        private static List<Card> Cards { get; set; } = [];

        /// <summary>
        /// Compte les points des cartes à grater gagnantes.
        /// </summary>
        /// <returns>Nombre de points.</returns>
        public static int CalculerPart1()
        {
            LoadCards();

            List<int> points = [];

            foreach (Card card in Cards)
            {
                points.Add((int)Math.Pow(2, CountWinningCards(card) - 1));
            }

            return points.Sum();
        }

        /// <summary>
        /// Accumule les cartes et retourne leur nombre total.
        /// </summary>
        /// <returns>Nombre de cartes.</returns>
        public static long CalculerPart2()
        {
            LoadCards();

            for (int i = 0; i < Cards.Count; i++)
            {
                int nbWin = CountWinningCards(Cards[i]);

                for (int j = 1; j < nbWin + 1; j++)
                {
                    Cards[i + j].CardCount += Cards[i].CardCount;
                }
            }

            return Cards.Sum(c => c.CardCount);
        }

        /// <summary>
        /// Retourne le nombre de numéros gagnants pour une carte.
        /// </summary>
        /// <param name="card">Une carte.</param>
        /// <returns>Nombre de numéros gagnants.</returns>
        private static int CountWinningCards(Card card)
        {
            return card.Numbers.Count(card.WinningNumbers.Contains);
        }

        /// <summary>
        /// Parse l'input et rempli <see cref="Cards"/>.
        /// </summary>
        private static void LoadCards()
        {
            Cards = [];

            List<string> lines = InputHandler.GetInputLines(InputPath);

            foreach (string line in lines)
            {
                Card card = new();

                string[] sGameAndCards = line.Split(":");

                int cardNumber = int.Parse(sGameAndCards[0][(sGameAndCards[0].LastIndexOf(' ') + 1)..]);
                card.CardNumber = cardNumber;

                string[] sWinningCardsAndCards = sGameAndCards[1].Split("|");
                string[] sWinningCards = sWinningCardsAndCards[0].Split(" ");
                string[] sCards = sWinningCardsAndCards[1].Split(" ");

                card.WinningNumbers = sWinningCards.Where(swc => !string.IsNullOrWhiteSpace(swc)).Select(int.Parse).ToList();
                card.Numbers = sCards.Where(sc => !string.IsNullOrWhiteSpace(sc)).Select(int.Parse).ToList();

                Cards.Add(card);
            }
        }

        /// <summary>
        /// Représente une carte à grater.
        /// </summary>
        private sealed class Card
        {
            /// <summary>
            /// Initialise une nouvelle instance de la classe <see cref="Card"/>.
            /// </summary>
            public Card()
            {
                Numbers = [];
                WinningNumbers = [];
            }

            /// <summary>
            /// Nombre d'exemplaire de la carte.
            /// </summary>
            public int CardCount { get; set; } = 1;

            /// <summary>
            /// Identifiant de la carte.
            /// </summary>
            public int CardNumber { get; set; }

            /// <summary>
            /// Numéros de la carte.
            /// </summary>
            public List<int> Numbers { get; set; }

            /// <summary>
            /// Numéros gagnants de la carte.
            /// </summary>
            public List<int> WinningNumbers { get; set; }
        }
    }
}