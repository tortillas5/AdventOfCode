using AdventOfCode.Utils;

namespace AdventOfCode.Days
{
    /// <summary>
    /// Jour 6 de advent of code.
    /// https://adventofcode.com/2023/day/6
    /// </summary>
    internal class Day06
    {
        /// <summary>
        /// Chemin vers l'input du jour 6.
        /// </summary>
        private static readonly string InputPath = Path.Combine(Environment.CurrentDirectory, "Inputs", "Day06.txt");

        /// <summary>
        /// The race the boat compete.
        /// </summary>
        public static (long Duration, long DistanceToBeat) Race { get; set; }

        /// <summary>
        /// The races the boat compete.
        /// </summary>
        public static List<(int Duration, int DistanceToBeat)> Races { get; set; } = [];

        /// <summary>
        /// Calculate the button holding values that beat the races, then multiply the count of theses values.
        /// </summary>
        /// <returns>Multiplication of the values beating the races.</returns>
        public static long CalculerPart1()
        {
            LoadRaces();

            List<int> beatenRaces = [];

            foreach (var (Duration, DistanceToBeat) in Races)
            {
                Boat boat = new();
                List<int> beatingValue = [];

                for (int i = 0; i < Duration; i++)
                {
                    boat.ButtonHoldingTime = i;

                    if (boat.MaxDist(Duration) > DistanceToBeat)
                    {
                        beatingValue.Add(i);
                    }
                }

                beatenRaces.Add(beatingValue.Count);
            }

            return beatenRaces.Aggregate((a, b) => a * b);
        }

        /// <summary>
        /// For the race, calculate the first winning value, the last winning value, then subtract them.
        /// </summary>
        /// <returns>Number of possibilities that win the race.</returns>
        public static long CalculerPart2()
        {
            LoadRace();

            Boat boat = new();

            long firstValue = long.MaxValue;
            long lastValue = long.MaxValue;

            for (long i = 0; i < Race.Duration; i++)
            {
                boat.ButtonHoldingTime = i;

                if (boat.MaxDist(Race.Duration) > Race.DistanceToBeat)
                {
                    firstValue = i;

                    break;
                }
            }

            for (long i = Race.Duration; i > 0; i--)
            {
                boat.ButtonHoldingTime = i;

                if (boat.MaxDist(Race.Duration) > Race.DistanceToBeat)
                {
                    lastValue = i;

                    break;
                }
            }

            return lastValue - firstValue + 1;
        }

        /// <summary>
        /// Charge la course dans <see cref="Race"/>.
        /// </summary>
        private static void LoadRace()
        {
            List<string> lines = InputHandler.GetInputLines(InputPath);

            var time = long.Parse(string.Join("", lines[0].Split(":")[1].Split(" ").Where(s => !string.IsNullOrWhiteSpace(s))));
            var distance = long.Parse(string.Join("", lines[1].Split(":")[1].Split(" ").Where(s => !string.IsNullOrWhiteSpace(s))));

            for (int i = 0; i < 4; i++)
            {
                Race = (time, distance);
            }
        }

        /// <summary>
        /// Charge les courses dans <see cref="Races"/>.
        /// </summary>
        private static void LoadRaces()
        {
            List<string> lines = InputHandler.GetInputLines(InputPath);

            var times = lines[0].Split(":")[1].Split(" ").Where(s => !string.IsNullOrWhiteSpace(s)).Select(int.Parse).ToArray();
            var distances = lines[1].Split(":")[1].Split(" ").Where(s => !string.IsNullOrWhiteSpace(s)).Select(int.Parse).ToArray();

            for (int i = 0; i < 4; i++)
            {
                Races.Add((times[i], distances[i]));
            }
        }

        /// <summary>
        /// A toy boat.
        /// </summary>
        internal class Boat
        {
            /// <summary>
            /// Speed of the boat.
            /// </summary>
            public long BoatSpeed
            { get { return ButtonHoldingTime; } }

            /// <summary>
            /// Duration the button of the boat was holed.
            /// </summary>
            public long ButtonHoldingTime { get; set; }

            /// <summary>
            /// Maximum distance the boat can reach within the race duration.
            /// </summary>
            /// <param name="raceDuration">Duration of the race.</param>
            /// <returns>Maximum distance the boat can reach.</returns>
            public long MaxDist(long raceDuration)
            {
                return (raceDuration - ButtonHoldingTime) * BoatSpeed;
            }
        }
    }
}