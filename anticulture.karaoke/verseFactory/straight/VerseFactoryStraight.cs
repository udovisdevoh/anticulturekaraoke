using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using anticulture.karaoke.themes;

namespace anticulture.karaoke.verseFactory
{
    /// <summary>
    /// Create straight verses from existing verses
    /// </summary>
    class VerseFactoryStraight
    {
        #region Constants
        /// <summary>
        /// Sampling size
        /// </summary>
        private const int SamplingSize = 200;
        #endregion

        #region Public Methods
        /// <summary>
        /// Build a straight verse
        /// </summary>
        /// <param name="previousVerse">previous verse</param>
        /// <returns>straight verse</returns>
        public static Verse Build(Verse previousVerse)
        {
            IEnumerable<Verse> verseList = VerseFactory.LyricSource.GetRandomSourceLineList(VerseFactory.Random, SamplingSize);
            return GetMostThemeRelatedVerseWithDesiredLength(verseList, VerseFactory.DesiredLength);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get best verse from verse list
        /// </summary>
        /// <param name="verseList">verse list</param>
        /// <param name="desiredLength">desired length</param>
        /// <returns>best verse</returns>
        private static Verse GetMostThemeRelatedVerseWithDesiredLength(IEnumerable<Verse> verseList, byte desiredLength)
        {
            Verse bestVerse = null;
            int bestScore = -1;
            int currentScore = 0;
            foreach (Verse currentVerse in verseList)
            {
                currentScore = Evaluator.GetScore(currentVerse, VerseFactory.ThemeList, VerseFactory.ThemeBlackList, desiredLength);
                if (currentScore > bestScore || bestVerse == null)
                {
                    bestScore = currentScore;
                    bestVerse = currentVerse;
                }
            }
            return bestVerse;
        }
        #endregion
    }
}