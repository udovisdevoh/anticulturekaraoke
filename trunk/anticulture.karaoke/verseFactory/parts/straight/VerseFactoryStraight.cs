﻿using System;
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
        public int samplingSize = 20000;
        #endregion

        #region Fields
        /// <summary>
        /// Verse construction settings
        /// </summary>
        protected VerseConstructionSettings verseConstructionSettings;

        /// <summary>
        /// Creation memory
        /// </summary>
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
                creationMemory.StraightSourceSampleVerseList = VerseConstructionSettings.LyricSource.GetRandomSourceLineList(verseConstructionSettings.Random, samplingSize);

            Verse bestVerse = GetMostThemeRelatedVerseWithDesiredLength(creationMemory.StraightSourceSampleVerseList, verseConstructionSettings.DesiredLength);

            creationMemory.StraightSourceSampleVerseList.Remove(bestVerse);

            return bestVerse;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Get best verse from verse list
        /// </summary>
        /// <param name="verseList">verse list</param>
        /// <param name="desiredLength">desired length</param>
        /// <returns>best verse</returns>
        protected Verse GetMostThemeRelatedVerseWithDesiredLength(IEnumerable<Verse> verseList, short desiredLength)
        {
            return GetMostThemeRelatedVerseWithDesiredLength(verseList, desiredLength, null);
        }

        /// <summary>
        /// Get best verse from verse list
        /// </summary>
        /// <param name="verseList">verse list</param>
        /// <param name="desiredLength">desired length</param>
        /// <param name="versesToRhymeWith">facultative verse to rhyme with (can be null)</param>
        /// <returns>best verse</returns>
        protected Verse GetMostThemeRelatedVerseWithDesiredLength(IEnumerable<Verse> verseList, short desiredLength, Queue<Verse> versesToRhymeWith)
        {
            Verse bestVerse = null;
            int bestScore = -1;
            int currentScore = 0;
            foreach (Verse currentVerse in verseList)
            {
                currentScore = Evaluator.GetScore(currentVerse, verseConstructionSettings.ThemeList, verseConstructionSettings.ThemeBlackList, desiredLength, creationMemory, versesToRhymeWith, verseConstructionSettings.Random);
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