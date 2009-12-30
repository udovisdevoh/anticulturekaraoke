using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.verseFactory
{
    class RhymeEvaluator
    {
        #region Constants
        private const int valueByRhyme = 4;
        #endregion

        #region Public Methods
        public int GetScore(Verse currentVerse, Queue<Verse> listVerseToRhymeWith)
        {
            int score = 0;

            string lastWord = currentVerse.WordList.Last();
            string lastWordToRhymeWith;
            string rhymePatternFound;
            foreach (Verse verseToRhymeWith in listVerseToRhymeWith)
            {
                lastWordToRhymeWith = verseToRhymeWith.WordList.Last();

                //Ignore rhyme if the words begin with same letter
                if (lastWord.Length > 0 && lastWordToRhymeWith.Length > 0 && lastWord[0] == lastWordToRhymeWith[0])
                    continue;

                rhymePatternFound = TryFindRhymePattern(lastWord, lastWordToRhymeWith);

                if (rhymePatternFound != null)
                {
                    score += valueByRhyme;
                    break;
                }
            }

            return score;
        }
        #endregion

        #region Private Methods
        private string TryFindRhymePattern(string word1, string word2)
        {
            #warning Implement TryFindRhymePattern()
            throw new NotImplementedException();
        }
        #endregion
    }
}
