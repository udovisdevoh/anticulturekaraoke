using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.verseFactory
{
    /// <summary>
    /// Generates char based markov chain based lyrics
    /// </summary>
    class VerseFactoryCryptic : AbstractVerseFactory
    {
        #region Constants
        /// <summary>
        /// Sampling size
        /// </summary>
        private const int SamplingSize = 2000;
        #endregion

        #region Fields
        /// <summary>
        /// Verse construction settings
        /// </summary>
        private VerseConstructionSettings verseConstructionSettings;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="verseConstructionSettings">verse construction settings</param>
        public VerseFactoryCryptic(VerseConstructionSettings verseConstructionSettings)
        {
            this.verseConstructionSettings = verseConstructionSettings;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Generates char based markov chain based verse
        /// </summary>
        /// <param name="previousVerse">previous verse</param>
        /// <returns>char based markov chain based verse</returns>
        public override Verse Build(Verse previousVerse)
        {
            IEnumerable<Verse> verseList = VerseConstructionSettings.LyricSource.GetRandomSourceLineList(VerseConstructionSettings.Random, SamplingSize);
            verseList = TryGetMostThemeRelatedVerseList(verseList);
            LetterMatrix letterMatrix = new LetterMatrix(verseList);

            string sentence = string.Empty;
            char currentLetter;
            do
            {
                currentLetter = letterMatrix.GenerateNextChar(VerseConstructionSettings.Random);
                sentence += currentLetter;
            } while (sentence.Length < verseConstructionSettings.DesiredLength || currentLetter != ' ');

            sentence = sentence.HardTrim();

            return new Verse(sentence);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Generate theme based word
        /// </summary>
        /// <param name="letterMatrix">letter matrix</param>
        /// <returns>theme based word</returns>
        private string GenerateThemeBasedWord(LetterMatrix letterMatrix)
        {
            string word = string.Empty;
            char currentLetter;
            do
            {
                currentLetter = letterMatrix.GenerateNextChar(VerseConstructionSettings.Random);
                word += currentLetter;
            } while (currentLetter != ' ');

            word = word.HardTrim();
            return word;
        }

        /// <summary>
        /// Try vers that contain desired theme words
        /// </summary>
        /// <param name="verseList">verse list</param>
        /// <returns>verse list with desired theme words</returns>
        private IEnumerable<Verse> TryGetMostThemeRelatedVerseList(IEnumerable<Verse> verseList)
        {
            List<Verse> themeRelatedVerseList = new List<Verse>();

            int currentScore;
            foreach (Verse currentVerse in verseList)
            {
                currentScore = Evaluator.GetScore(currentVerse, verseConstructionSettings.ThemeList, verseConstructionSettings.ThemeBlackList);
                if (currentScore > 0)
                {
                    themeRelatedVerseList.Add(currentVerse);
                }
            }

            if (themeRelatedVerseList.Count > verseConstructionSettings.DesiredLength / 3)
                return themeRelatedVerseList;
            else
                return verseList;
        }
        #endregion
    }
}