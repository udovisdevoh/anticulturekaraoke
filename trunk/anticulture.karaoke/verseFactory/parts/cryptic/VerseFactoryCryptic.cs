﻿using System;
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
        private const int SamplingSize = 200;
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
            return letterMatrix.BuildSentence(verseConstructionSettings.DesiredLength,VerseConstructionSettings.Random);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Try vers that contain desired theme words
        /// </summary>
        /// <param name="verseList">verse list</param>
        /// <returns>verse list with desired theme words</returns>
        private IEnumerable<Verse> TryGetMostThemeRelatedVerseList(IEnumerable<Verse> verseList)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}