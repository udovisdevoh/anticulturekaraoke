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
    class VerseFactoryStraight : AbstractVerseFactory
    {
        #region Constants
        /// <summary>
        /// Sampling size
        /// </summary>
        private const int SamplingSize = 20000;
        #endregion

        #region Fields
        /// <summary>
        /// Verse construction settings
        /// </summary>
        protected VerseConstructionSettings verseConstructionSettings;

        protected CreationMemory creationMemory;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="verseConstructionSettings">verse construction settings</param>
        public VerseFactoryStraight(VerseConstructionSettings verseConstructionSettings, CreationMemory creationMemory)
        {
            this.verseConstructionSettings = verseConstructionSettings;
            this.creationMemory = creationMemory;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Build a straight verse
        /// </summary>
        /// <param name="previousVerse">previous verse</param>
        /// <returns>straight verse</returns>
        public override Verse Build(Verse previousVerse)
        {
            if (creationMemory.StraightSourceSampleVerseList == null || creationMemory.StraightSourceSampleVerseList.Count < 1)
                creationMemory.StraightSourceSampleVerseList = VerseConstructionSettings.LyricSource.GetRandomSourceLineList(VerseConstructionSettings.Random, SamplingSize);

            Verse bestVerse = GetMostThemeRelatedVerseWithDesiredLength(creationMemory.StraightSourceSampleVerseList, verseConstructionSettings.DesiredLength);

            creationMemory.StraightSourceSampleVerseList.Remove(bestVerse);

            return bestVerse;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get best verse from verse list
        /// </summary>
        /// <param name="verseList">verse list</param>
        /// <param name="desiredLength">desired length</param>
        /// <returns>best verse</returns>
        private Verse GetMostThemeRelatedVerseWithDesiredLength(IEnumerable<Verse> verseList, short desiredLength)
        {
            Verse bestVerse = null;
            int bestScore = -1;
            int currentScore = 0;
            foreach (Verse currentVerse in verseList)
            {
                currentScore = Evaluator.GetScore(currentVerse, verseConstructionSettings.ThemeList, verseConstructionSettings.ThemeBlackList, desiredLength, creationMemory);
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