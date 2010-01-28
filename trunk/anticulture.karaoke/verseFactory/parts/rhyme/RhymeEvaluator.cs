using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace anticulture.karaoke.verseFactory
{
    class RhymeEvaluator
    {
        #region Constants
        private const int valueByRhyme = 11;

        private const string phoneticTableFile = "phoneticTable.dat.txt";

        private const string rhymeChartFile = "rhymeChart.dat.txt";

        private const int howManyPhoneticSymbolForEnding = 2;

        private const int howManyEnglishLetterForEnding = 4;
        #endregion

        #region Parts
        private PhoneticTable phoneticTable = new PhoneticTable(phoneticTableFile);

        private Dictionary<string, string> rhymeChart;
        #endregion

        #region Constructor
        public RhymeEvaluator()
        {
            rhymeChart = BuildRhymeChart(rhymeChartFile);
        }
        #endregion

        #region Public Methods
        public int GetScore(Verse currentVerse, Queue<Verse> listVerseToRhymeWith)
        {
            int score = 0;

            string lastWord = currentVerse.WordList.Last();
            string lastWordToRhymeWith;
            foreach (Verse verseToRhymeWith in listVerseToRhymeWith)
            {
                lastWordToRhymeWith = verseToRhymeWith.WordList.Last();

                //Ignore rhyme if the words begin with same letter
                if (lastWord.Length > 0 && lastWordToRhymeWith.Length > 0 && lastWord[0] == lastWordToRhymeWith[0])
                    continue;

                if (IsRhymeWith(lastWord, lastWordToRhymeWith))
                {
                    score += valueByRhyme;
                    break;
                }
            }

            return score;
        }
        #endregion

        #region Private Methods
        private bool IsRhymeWith(string word1, string word2)
        {
            if (word1 == word2)
                return false;

            string ending1, ending2;
            if (rhymeChart.TryGetValue(word1, out ending1))
            {
                if (rhymeChart.TryGetValue(word2, out ending2))
                {
                    if (ending1 == ending2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private Dictionary<string, string> BuildRhymeChart(string rhymeChartFile)
        {
            Dictionary<string, string> rhymeChart = new Dictionary<string,string>();
            string line;
            using (StreamReader streamReader = new StreamReader(rhymeChartFile))
            {
                while (true)
                {
                    line = streamReader.ReadLine();
                    if (line == null)
                        break;

                    if (line.Length > 1)
                        rhymeChart.Add(GetWord(line), GetRhymeCategory(line));
                }
            }
            return rhymeChart;
        }

        private string GetRhymeCategory(string line)
        {
            line = line.Substring(line.IndexOf(':') + 1).Trim();
            return line;
        }

        private string GetWord(string line)
        {
            line = line.Substring(0, line.IndexOf(':')).Trim();
            return line;
        }
        #endregion
    }
}