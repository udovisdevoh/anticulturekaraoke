using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using anticulture.karaoke.themes;

namespace anticulture.karaoke
{
    public static class Probabilities
    {
        /// <summary>
        /// Get random key from row according to probabilities in value
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="row">row</param>
        /// <returns>random key from row according to probabilities in value</returns>
        public static string GetPonderatedRandom(this Dictionary<string, int> row, Random random)
        {
            int totalDiscreteSum = Sum(row.Values);
            int randomInt = random.Next(0,totalDiscreteSum);
            int counter = 0;

            string text;
            int probability;
            string selectedValue = null;

            foreach (KeyValuePair<string, int> textAndProbability in row)
            {
                text = textAndProbability.Key;
                probability = textAndProbability.Value;

                selectedValue = text;

                counter += probability;

                if (counter > randomInt)
                    break;
            }
            return selectedValue;
        }

        /// <summary>
        /// Get random key from row according to probabilities in value
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="row">row</param>
        /// <returns>random key from row according to probabilities in value</returns>
        public static char GetPonderatedRandom(this Dictionary<char, int> row, Random random)
        {
            int totalDiscreteSum = Sum(row.Values);
            int randomInt = random.Next(0, totalDiscreteSum);
            int counter = 0;

            char letter;
            int probability;
            char selectedValue = ' ';

            foreach (KeyValuePair<char, int> textAndProbability in row)
            {
                letter = textAndProbability.Key;
                probability = textAndProbability.Value;

                selectedValue = letter;

                counter += probability;

                if (counter > randomInt)
                    break;
            }
            return selectedValue;
        }

        /// <summary>
        /// Sum of numbers
        /// </summary>
        /// <param name="intSet">number list</param>
        /// <returns>sum of numbers</returns>
        public static int Sum(IEnumerable<int> intSet)
        {
            int sum = 0;
            foreach (int number in intSet)
                sum += number;
            return sum;
        }
    }
}
