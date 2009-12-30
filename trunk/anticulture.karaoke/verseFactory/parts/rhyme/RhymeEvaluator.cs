using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.verseFactory
{
    class RhymeEvaluator
    {
        #region Constants
        private const int valueByRhyme = 11;

        private const string phoneticTableFile = "phoneticTable.dat.txt";

        private const int howManyPhoneticSymbolForEnding = 2;
        #endregion

        #region Parts
        private PhoneticTable phoneticTable = new PhoneticTable(phoneticTableFile);
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

                if (IsRhymeWith(lastWord,lastWordToRhymeWith))
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
            string phoneticValue1 = phoneticTable.GetPhoneticValueOf(word1);
            string phoneticValue2 = phoneticTable.GetPhoneticValueOf(word2);

            #warning Remove comments
            //if (phoneticValue1 == null)
            //    phoneticValue1 = GetPhoneticValueOfWordUsingSimilarWordEnding(word1);
            //if (phoneticValue2 == null)
            //    phoneticValue2 = GetPhoneticValueOfWordUsingSimilarWordEnding(word2);

            if (phoneticValue1 == null || phoneticValue2 == null)
                return false;

            return GetPhoneticEnding(phoneticValue1) == GetPhoneticEnding(phoneticValue2);
        }

        private string GetPhoneticEnding(string phoneticValue)
        {
            if (!phoneticValue.Contains(' '))
                return phoneticValue;

            string[] wordList = phoneticValue.Split(' ');

            string ending = string.Empty;

            int key;
            for (int i = 0; i < howManyPhoneticSymbolForEnding; i++)
            {
                key = wordList.Length - i - 1;
                if (key > 0 && key < wordList.Length)
                    ending = wordList[key] + " " + ending;
            }

            return ending.Trim();
        }
        #endregion
    }
}