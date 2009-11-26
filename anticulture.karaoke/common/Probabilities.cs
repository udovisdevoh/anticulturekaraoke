using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.common
{
    public static class Probabilities
    {
        /// <summary>
        /// Get random key from row according to probabilities in value
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="row">row</param>
        /// <returns>random key from row according to probabilities in value</returns>
        public static string GetPonderatedRandom(this Dictionary<string, double> row, Random random)
        {
            double totalScallarSum = row.Sum(element => element.Value);
            double randomDouble = random.NextDouble() * totalScallarSum;
            double counter = 0.0;

            string text;
            double probability;
            string selectedValue = null;

            foreach (KeyValuePair<string, double> textAndProbability in row)
            {
                text = textAndProbability.Key;
                probability = textAndProbability.Value;

                selectedValue = text;

                counter += probability;

                if (counter > randomDouble)
                    break;
            }
            return selectedValue;
        }
    }
}
