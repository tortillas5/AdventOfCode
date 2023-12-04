using AdventOfCode.Utils;

namespace AdventOfCode
{
    internal class Day04
    {
        private static readonly string InputPath = Path.Combine(Environment.CurrentDirectory, "Inputs", "Day04.txt");

        private static List<Card>? Cards { get; set; }

        public static int CalculerPart1()
        {
            LoadCards();

            List<int> points = [];

            foreach (Card card in Cards)
            {
                int nbWin = card.Numbers.Count(card.WinningNumbers.Contains);

                if (nbWin == 1)
                {
                    points.Add(nbWin);
                }
                else
                {
                    points.Add((int)Math.Pow(2, nbWin - 1));
                }
            }

            return points.Sum();
        }

        public static long CalculerPart2()
        {
            LoadCards();

            for (int i = 0; i < Cards.Count; i++)
            {
                Cards[i].NbWin = GetNbWinCard(Cards[i]);
            }

            return GetNbWinningCards(Cards.Where(c => c.NbWin > 0).ToList());
        }

        private static int GetNbWinCard(Card card)
        {
            return card.Numbers.Count(card.WinningNumbers.Contains);
        }

        private static long GetNbWinningCards(List<Card> winningCards)
        {
            List<Card> winCards = [];

            foreach (var winningCard in winningCards)
            {
                for (int i = 1; i < winningCard.NbWin + 1; i++)
                {
                    if (winningCard.CardNumber + i < Cards.Count)
                    {
                        Card card = Cards.FirstOrDefault(c => c.CardNumber == winningCard.CardNumber + i);

                        if (card != null && card.NbWin > 0)
                        {
                            winCards.Add(card);
                        }
                    }
                }
            }

            return winningCards.Count + GetNbWinningCards(winCards.Where(wc => wc.NbWin > 0).ToList());
        }

        private static void LoadCards()
        {
            Cards = [];

            List<string> lines = InputHandler.GetInputLines(InputPath);

            foreach (string line in lines)
            {
                Card card = new();

                string[] sGameAndCards = line.Split(":");

                int cardNumber = int.Parse(sGameAndCards[0].Split(" ").Last());
                card.CardNumber = cardNumber;

                string[] sWinningCardsAndCards = sGameAndCards[1].Split("|");
                string[] sWinningCards = sWinningCardsAndCards[0].Split(" ");
                string[] sCards = sWinningCardsAndCards[1].Split(" ");

                card.WinningNumbers = sWinningCards.Where(swc => !string.IsNullOrWhiteSpace(swc)).Select(int.Parse).ToList();
                card.Numbers = sCards.Where(sc => !string.IsNullOrWhiteSpace(sc)).Select(int.Parse).ToList();

                Cards.Add(card);
            }
        }

        private class Card
        {
            public int CardNumber { get; set; }
            public List<int> Numbers { get; set; }
            public List<int> WinningNumbers { get; set; }
            public int NbWin { get; set; }
        }
    }
}