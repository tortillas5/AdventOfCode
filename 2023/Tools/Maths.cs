namespace AdventOfCode.Tools
{
    /// <summary>
    /// Class containing mathematical methods.
    /// </summary>
    internal static class Maths
    {
        /// <summary>
        /// Return the least common multiple for an array of numbers.
        /// </summary>
        /// <param name="numbers">Array of numbers.</param>
        /// <returns>Least common multiple of the array of number.</returns>
        public static long LeastCommonMultiple(long[] numbers)
        {
            long lcm = 1;
            long divisor = 2;

            while (true)
            {
                long counter = 0;
                bool divisible = false;

                for (long i = 0; i < numbers.Length; i++)
                {
                    if (numbers[i] == 0)
                    {
                        return 0;
                    }
                    else if (numbers[i] < 0)
                    {
                        numbers[i] = numbers[i] * (-1);
                    }

                    if (numbers[i] == 1)
                    {
                        counter++;
                    }

                    if (numbers[i] % divisor == 0)
                    {
                        divisible = true;
                        numbers[i] = numbers[i] / divisor;
                    }
                }

                if (divisible)
                {
                    lcm *= divisor;
                }
                else
                {
                    divisor++;
                }

                if (counter == numbers.Length)
                {
                    return lcm;
                }
            }
        }
    }
}