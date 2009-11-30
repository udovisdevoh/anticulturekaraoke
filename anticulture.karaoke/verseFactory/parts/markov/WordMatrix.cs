using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.verseFactory
{
    /// <summary>
    /// Word markov matrix
    /// </summary>
    class WordMatrix
    {
        #region Fields
        /// <summary>
        /// Previous word
        /// </summary>
        private string previousWord;

        /// <summary>
        /// Previous previous word
        /// </summary>
        private string previousPreviousWord;

        /// <summary>
        /// Integer based word matrix (key: pair of word, value: count)
        /// </summary>
        private Dictionary<string, Dictionary<string,int>> absoluteMatrix;

        /// <summary>
        /// Double based word matrix (key: pair of word, value: probability)
        /// </summary>
        private Dictionary<string, Dictionary<string, double>> relativeMatrix;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a word matrix from verse list
        /// </summary>
        /// <param name="verseList">verse list</param>
        public WordMatrix(IEnumerable<Verse> verseList)
        {
            resetCursor();
            absoluteMatrix = new Dictionary<string, Dictionary<string, int>>();
            foreach (Verse verse in verseList)
                Learn(absoluteMatrix, verse.ToString() + " [stop]");
            relativeMatrix = Normalize(absoluteMatrix);
        }

        /// <summary>
        /// Create a word matrix from verse lists
        /// </summary>
        /// <param name="verseList1">1st verse list</param>
        /// <param name="verseList2">2nd verse list</param>
        public WordMatrix(IEnumerable<Verse> verseList1, IEnumerable<Verse> verseList2)
        {
            resetCursor();
            absoluteMatrix = new Dictionary<string, Dictionary<string, int>>();
            foreach (Verse verse in verseList1)
                Learn(absoluteMatrix, verse.ToString() + " [stop]");
            foreach (Verse verse in verseList2)
                Learn(absoluteMatrix, verse.ToString() + " [stop]");
            relativeMatrix = Normalize(absoluteMatrix);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Reset cursor to begining
        /// </summary>
        public void resetCursor()
        {
            previousWord = "[start]";
            previousPreviousWord = "[start]";
        }

        /// <summary>
        /// Generate next word
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>next word</returns>
        public string GenerateNextWord(Random random)
        {
            Dictionary<string, double> row;
            if (!relativeMatrix.TryGetValue(previousPreviousWord + " " + previousWord, out row))
                return null;
            string selectedWord = row.GetPonderatedRandom(random);

            previousPreviousWord = previousWord;
            previousWord = selectedWord;

            return selectedWord;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Normalize absolute matrix into relative matrix
        /// </summary>
        /// <param name="absoluteMatrix">absolute matrix</param>
        /// <returns>relative matrix</returns>
        private Dictionary<string, Dictionary<string, double>> Normalize(Dictionary<string, Dictionary<string, int>> absoluteMatrix)
        {
            Dictionary<string, Dictionary<string, double>> normalizedMatrix = new Dictionary<string, Dictionary<string, double>>();
            foreach (KeyValuePair<string, Dictionary<string, int>> sourceAndRow in absoluteMatrix)
            {
                string source = sourceAndRow.Key;
                Dictionary<string, int> row = sourceAndRow.Value;
                normalizedMatrix.Add(source, Normalize(row));
            }
            return normalizedMatrix;
        }

        /// <summary>
        /// Normalize an absolute row
        /// </summary>
        /// <param name="absoluteRow">absolute row</param>
        /// <returns>relative row</returns>
        private Dictionary<string, double> Normalize(Dictionary<string, int> absoluteRow)
        {
            //int sum = absoluteRow.Sum(currentRow => currentRow.Value);
            int sum = Sum(absoluteRow.Values);
            Dictionary<string, double> normalizedRow = new Dictionary<string, double>();

            foreach (KeyValuePair<string, int> textAndValue in absoluteRow)
            {
                string text = textAndValue.Key;
                int value = textAndValue.Value;
                normalizedRow.Add(text,((double)(value)) / ((double)(sum)));
            }
            return normalizedRow;
        }

        private int Sum(IEnumerable<int> set)
        {
            int sum = 0;
            foreach (int number in set)
                sum += number;
            return sum;
        }

        /// <summary>
        /// Learn line into absolute matrix
        /// </summary>
        /// <param name="absoluteMatrix">absolute matrix</param>
        /// <param name="line">line</param>
        private void Learn(Dictionary<string, Dictionary<string, int>> absoluteMatrix, string line)
        {
            string currentPreviousPreviousWord = "[start]";
            string currentPreviousWord = "[start]";
            string[] words = line.Split(' ');
            string word;
            int currentValue;
            foreach (string currentWord in words)
            {
                word = currentWord.HardTrim();

                string sourcePair = currentPreviousPreviousWord + " " + currentPreviousWord;

                Dictionary<string, int> currentRow;
                if (!absoluteMatrix.TryGetValue(sourcePair, out currentRow))
                {
                    currentRow = new Dictionary<string, int>();
                    absoluteMatrix.Add(sourcePair, currentRow);
                }

                if (!currentRow.TryGetValue(word, out currentValue))
                    currentRow.Add(word, 0);

                if (word == "[stop")
                {
                }

                currentRow[word]++;

                currentPreviousPreviousWord = currentPreviousWord;
                currentPreviousWord = word;
            }
        }
        #endregion
    }
}