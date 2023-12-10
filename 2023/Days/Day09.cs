using AdventOfCode.Tools;

namespace AdventOfCode.Days
{
    /// <summary>
    /// Day 9 of the advent of code.
    /// https://adventofcode.com/2023/day/9
    /// </summary>
    internal static class Day09
    {
        /// <summary>
        /// Path of the day 9 input.
        /// </summary>
        private static readonly string InputPath = Path.Combine(Environment.CurrentDirectory, "Inputs", "Day09.txt");

        public static List<List<int>> OasisNumbers { get; set; } = [];

        public static long CalculatePart1()
        {
            LoadOasisDatas();

            List<int> result = [];
            List<List<List<int>>> differencesPerData = [];

            foreach (List<int> oasisData in OasisNumbers)
            {
                differencesPerData.Add(GetDifferences(oasisData).Prepend(oasisData).ToList());
            }

            foreach (List<List<int>> datas in differencesPerData)
            {
                result.Add(Extrapolate(datas));
            }

            return result.Sum();
        }

        public static long CalculatePart2()
        {
            LoadOasisDatas();

            List<int> result = [];
            List<List<List<int>>> differencesPerData = [];

            foreach (List<int> oasisData in OasisNumbers)
            {
                differencesPerData.Add(GetDifferences(oasisData).Prepend(oasisData).ToList());
            }

            foreach (List<List<int>> datas in differencesPerData)
            {
                result.Add(ReverseExtrapolate(datas));
            }

            return result.Sum();
        }

        private static int Extrapolate(List<List<int>> datas)
        {
            for (int i = datas.Count - 1; i >= 0; i--)
            {
                if (i == datas.Count - 1)
                {
                    datas[i].Add(0);
                }
                else
                {
                    datas[i].Add(datas[i][^1] + datas[i + 1][^1]);
                }
            }

            return datas[0][^1];
        }

        private static List<List<int>> GetDifferences(List<int> datas)
        {
            List<List<int>> differences = [];
            List<int> difference = [];

            for (int i = 0; i < datas.Count - 1; i++)
            {
                difference.Add(datas[i + 1] - datas[i]);
            }

            differences.Add(difference);

            if (difference.Exists(d => d != 0))
            {
                differences.AddRange(GetDifferences(difference));
            }

            return differences;
        }

        private static void LoadOasisDatas()
        {
            OasisNumbers = [];

            foreach (var line in InputHandler.GetInputLines(InputPath).Where(s => !string.IsNullOrWhiteSpace(s)))
            {
                OasisNumbers.Add(line.Split(' ').Select(int.Parse).ToList());
            }
        }

        private static int ReverseExtrapolate(List<List<int>> datas)
        {
            for (int i = datas.Count - 1; i >= 0; i--)
            {
                if (i == datas.Count - 1)
                {
                    datas[i] = datas[i].Prepend(0).ToList();
                }
                else
                {
                    datas[i] = datas[i].Prepend(datas[i][0] - datas[i + 1][0]).ToList();
                }
            }

            return datas[0][0];
        }
    }
}