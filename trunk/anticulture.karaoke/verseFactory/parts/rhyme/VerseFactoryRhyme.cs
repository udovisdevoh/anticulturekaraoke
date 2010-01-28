using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.verseFactory
{
    class VerseFactoryRhyme : VerseFactoryStraight
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="verseConstructionSettings">verse construction settings</param>
        public VerseFactoryRhyme(VerseConstructionSettings verseConstructionSettings, CreationMemory creationMemory)
            : base(verseConstructionSettings, creationMemory)
        {
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Build a rhyme verse
        /// </summary>
        /// <param name="previousVerse">previous verse</param>
        /// <returns>straight verse</returns>
        public override Verse Build(Verse previousVerse)
        {
            if (creationMemory.StraightSourceSampleVerseList == null || creationMemory.StraightSourceSampleVerseList.Count < 1)
                creationMemory.StraightSourceSampleVerseList = VerseConstructionSettings.LyricSource.GetRandomSourceLineList(verseConstructionSettings.Random, samplingSize);

            if (creationMemory.VerseListToRhymeWith.Count > 8)
                creationMemory.VerseListToRhymeWith.Clear();

            Verse bestVerse = GetMostThemeRelatedVerseWithDesiredLength(creationMemory.StraightSourceSampleVerseList, verseConstructionSettings.DesiredLength, creationMemory.VerseListToRhymeWith);

            creationMemory.StraightSourceSampleVerseList.Remove(bestVerse);

            return bestVerse;
        }
        #endregion
    }
}