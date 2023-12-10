using AdventOfCode.Tools;

namespace AdventOfCode.Days
{
    /// <summary>
    /// Day 8 of the advent of code.
    /// https://adventofcode.com/2023/day/8
    /// </summary>
    internal class Day08
    {
        /// <summary>
        /// Path of the day 8 input.
        /// </summary>
        private static readonly string InputPath = Path.Combine(Environment.CurrentDirectory, "Inputs", "Day08.txt");

        /// <summary>
        /// string containing L's and R's to indicate if next step is Left or Right.
        /// </summary>
        public static string NavigationInstruction { get; set; } = string.Empty;

        /// <summary>
        /// Liste of navigation nodes.
        /// </summary>
        public static Node[] Nodes { get; set; } = [];

        /// <summary>
        /// Calculate the minimum number of steps needed to go from the node AAA to the node ZZZ.
        /// </summary>
        /// <returns>Minimum number of steps.</returns>
        public static long CalculatePart1()
        {
            Load();

            int stepCount = 0;
            bool continueNavigation = true;
            string step = "AAA";

            while (continueNavigation)
            {
                foreach (char navigation in NavigationInstruction)
                {
                    stepCount++;

                    if (navigation == 'L')
                    {
                        step = Nodes.First(n => n.Name == step).NameLeft;
                    }

                    if (navigation == 'R')
                    {
                        step = Nodes.First(n => n.Name == step).NameRight;
                    }

                    if (step == "ZZZ")
                    {
                        continueNavigation = false;
                    }
                }
            }

            return stepCount;
        }

        /// <summary>
        /// For each node ending with the letter A, count the steps need to end on a node ending with the letter Z.
        /// Then calculate what is the least common multiple these steps and return the result.
        /// </summary>
        /// <returns>A big number.</returns>
        public static long CalculatePart2()
        {
            Load();

            Step[] steps = Nodes.Where(n => n.Name.EndsWith('A')).Select(n => new Step(n.Name, 0)).ToArray();

            for (int i = 0; i < steps.Length; i++)
            {
                bool continueNavigation = true;
                string step = steps[i].Name;

                while (continueNavigation)
                {
                    foreach (char navigation in NavigationInstruction)
                    {
                        steps[i].StepCount = steps[i].StepCount + 1;

                        if (navigation == 'L')
                        {
                            step = Nodes.First(n => n.Name == step).NameLeft;
                        }

                        if (navigation == 'R')
                        {
                            step = Nodes.First(n => n.Name == step).NameRight;
                        }

                        if (step.EndsWith('Z'))
                        {
                            continueNavigation = false;
                        }
                    }
                }
            }

            return Maths.LeastCommonMultiple(steps.Select(s => s.StepCount).ToArray());
        }

        /// <summary>
        /// Load the input into <see cref="NavigationInstruction"/> and <see cref="Nodes"/>.
        /// </summary>
        private static void Load()
        {
            List<Node> nodes = [];

            NavigationInstruction = InputHandler.GetInputLines(InputPath)[0];

            foreach (var line in InputHandler.GetInputLines(InputPath).Skip(1).Where(s => !string.IsNullOrWhiteSpace(s)))
            {
                var nameAndPathes = line.Split('=');
                var leftRight = nameAndPathes[1].Replace("(", string.Empty).Replace(")", string.Empty).Split(',');

                nodes.Add(new Node(nameAndPathes[0].Trim(), leftRight[0].Trim(), leftRight[1].Trim()));
            }

            Nodes = [.. nodes];
        }

        /// <summary>
        /// Node object.
        /// </summary>
        /// <param name="name">Name of the node.</param>
        /// <param name="nameLeft">Name of the left navigating node.</param>
        /// <param name="nameRight">Name of the right navigating node.</param>
        internal class Node(string name, string nameLeft, string nameRight)
        {
            /// <summary>
            /// Name of the node.
            /// </summary>
            public string Name => name;

            /// <summary>
            /// Name of the left navigating node.
            /// </summary>
            public string NameLeft => nameLeft;

            /// <summary>
            /// Name of the right navigating node.
            /// </summary>
            public string NameRight => nameRight;
        }

        /// <summary>
        /// A step.
        /// </summary>
        internal class Step
        {
            /// <summary>
            /// Initialize a new instance of <see cref="Step"/>.
            /// </summary>
            /// <param name="name">Name of the step.</param>
            /// <param name="stepCount">Steps done.</param>
            public Step(string name, long stepCount)
            {
                Name = name;
                StepCount = stepCount;
            }

            /// <summary>
            /// Initialize a new instance of <see cref="Step"/>.
            /// </summary>
            private Step()
            {
                Name = string.Empty;
            }

            /// <summary>
            /// Name of the step (like the name of a node).
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Total steps done.
            /// </summary>
            public long StepCount { get; set; }
        };
    }
}