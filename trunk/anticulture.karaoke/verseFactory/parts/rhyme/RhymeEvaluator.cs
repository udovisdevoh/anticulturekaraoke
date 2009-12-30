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

        private const int howManyEnglishLetterForEnding = 4;
        #endregion

        #region Parts
        private PhoneticTable phoneticTable = new PhoneticTable(phoneticTableFile);

        private SimilarPhoneticValueCache similarPhoneticValueCache = new SimilarPhoneticValueCache();

        private BoolRhymeCache boolRhymeCache = new BoolRhymeCache();
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

                #warning Fill boolRhymeCache with prerendered information

                bool isRhymeWith = false;
                if (boolRhymeCache.ContainsRhymeInfoAbout(lastWord,lastWordToRhymeWith))
                {
                    isRhymeWith = boolRhymeCache.IsRhymeWith(lastWord,lastWordToRhymeWith);
                }
                else
                {
                    isRhymeWith = IsRhymeWith(lastWord, lastWordToRhymeWith);
                    boolRhymeCache.AddRhymeInfo(lastWord, lastWordToRhymeWith, isRhymeWith);
                }

                if (isRhymeWith)
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

            if (phoneticValue1 == null)
                phoneticValue1 = GetPhoneticValueOfWordUsingSimilarWordEnding(word1);
            if (phoneticValue2 == null)
                phoneticValue2 = GetPhoneticValueOfWordUsingSimilarWordEnding(word2);

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

        private string GetPhoneticValueOfWordUsingSimilarWordEnding(string word)
        {
            string englishEnding;
            if (word.Length >= howManyEnglishLetterForEnding)
                englishEnding = word.Substring(word.Length - howManyEnglishLetterForEnding);
            else
                englishEnding = word;

            string similarWord = GetWordEndsWith(englishEnding);

            if (similarWord == null)
                return null;

            string valueFromCache = similarPhoneticValueCache.TryGetPhoneticValueOf(similarWord);

            if (valueFromCache == null)
            {
                valueFromCache = phoneticTable.GetPhoneticValueOf(similarWord);
                if (valueFromCache == null)
                    valueFromCache = "null";
                similarPhoneticValueCache.Add(similarWord, valueFromCache);
            }
            else
            {
            }

            if (valueFromCache == "null")
                valueFromCache = null;

            return valueFromCache;
        }

        private string GetWordEndsWith(string englishEnding)
        {
            foreach (HomophoneGroup homophoneGroup in phoneticTable)
            {
                foreach (string currentWord in homophoneGroup)
                {
                    if (currentWord.EndsWith(englishEnding))
                    {
                        return phoneticTable.GetPhoneticValueOf(currentWord);
                    }
                }
            }
            return null;
        }
        #endregion
    }
}