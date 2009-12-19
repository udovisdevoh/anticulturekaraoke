using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.verseFactory
{
    class VerseFactorySplice : VerseFactoryStraight
    {
        #region Constants
        public const int samplingWinnerSize = 20;

        public const int samplingContinuingSize = 20;
        #endregion

        #region Constructors
        /// <summary>
        /// Builds analogic verse
        /// </summary>
        /// <param name="verseConstructionSettings">verse construction settings</param>
        public VerseFactorySplice(VerseConstructionSettings verseConstructionSettings, CreationMemory creationMemory)
            : base(verseConstructionSettings, creationMemory)
        {
            this.samplingSize = 500;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Build spliced verse
        /// </summary>
        /// <param name="previousVerse">previous verse</param>
        /// <returns>analogic verse</returns>
        public override Verse Build(Verse previousVerse)
        {
            if (creationMemory.SplicedVerseList == null || creationMemory.SplicedVerseList.Count < 1)
                creationMemory.SplicedVerseList = BuildSplicedVerseList(previousVerse);

            return GetMostThemeRelatedVerseWithDesiredLength(creationMemory.SplicedVerseList, verseConstructionSettings.DesiredLength);
        }
        #endregion

        #region Private Methods
        private ICollection<Verse> GetShortVerseListToKeep(float lengthScalar, Verse previousVerse)
        {
            ICollection<Verse> verseListToKeep = new List<Verse>();
            for (int i = 0; i < samplingWinnerSize; i++)
            {
                Verse verse = Build(previousVerse, lengthScalar);
                if (verse != null)
                    verseListToKeep.Add(verse);
            }
            return verseListToKeep;
        }

        private Verse Build(Verse previousVerse, float lengthScalar)
        {
            short desiredLength = (short)(lengthScalar * (float)(verseConstructionSettings.DesiredLength));
            if (creationMemory.StraightSourceSampleVerseList == null || creationMemory.StraightSourceSampleVerseList.Count < 1)
                creationMemory.StraightSourceSampleVerseList = VerseConstructionSettings.LyricSource.GetRandomSourceLineList(verseConstructionSettings.Random, samplingSize);
            Verse bestVerse = GetMostThemeRelatedVerseWithDesiredLength(creationMemory.StraightSourceSampleVerseList, desiredLength);
            creationMemory.StraightSourceSampleVerseList.Remove(bestVerse);
            return bestVerse;
        }

        private ICollection<Verse> BuildSplicedVerseList(Verse previousVerse)
        {
            ICollection<Verse> verseList = GetShortVerseListToKeep(0.333f, previousVerse); //TODO, remove null return
            verseList = TrimShortVerseList(verseList, verseConstructionSettings.DesiredLength, 0.5f); //TODO, remove null return
            verseList = ExtendShortVerse(verseList, verseConstructionSettings.DesiredLength); //TODO, remove null return
            return verseList;
        }

        private ICollection<Verse> TrimShortVerseList(ICollection<Verse> verseList, short totalDesiredLength, float lengthScallar)
        {
            short halfLengthMax = (short)((float)(totalDesiredLength) * lengthScallar);
            foreach (Verse verse in verseList)
                while (verse.Length > halfLengthMax && verse.ToString().Trim().Contains(' '))
                    verse.RemoveLastWord();
            return verseList;
        }

        private ICollection<Verse> ExtendShortVerse(ICollection<Verse> verseList, short totalDesiredLength)
        {
            ICollection<Verse> extendedVerseList = new HashSet<Verse>();
            foreach (Verse verse in verseList)
            {
                Verse extendedVerse = ExtendShortVerse(verse, totalDesiredLength);
                if (extendedVerse != null)
                    extendedVerseList.Add(extendedVerse);
            }
            return extendedVerseList;
        }

        private Verse ExtendShortVerse(Verse startingVerse, short totalDesiredLength)
        {
            IEnumerable<Verse> extenstionVerseList = GetExtenstionVerseList(startingVerse.WordList.Last());
            ICollection<Verse> extendedVerseList = new HashSet<Verse>();

            foreach (Verse extensionVerse in extenstionVerseList)
            {
                Verse mergedVerse = MergeVerses(startingVerse, extensionVerse);
                if (mergedVerse != null)
                    extendedVerseList.Add(mergedVerse);
            }

            return GetMostThemeRelatedVerseWithDesiredLength(extendedVerseList, verseConstructionSettings.DesiredLength);
        }

        private IEnumerable<Verse> GetExtenstionVerseList(string startingWord)
        {
            return VerseConstructionSettings.LyricSource.GetRandomContiguousSourceLineList(verseConstructionSettings.Random, samplingContinuingSize, startingWord, true);
        }

        private Verse MergeVerses(Verse startingVerse, Verse extensionVerse)
        {
            return new Verse(startingVerse.ToString().RemoveLastWord() + " " + extensionVerse.ToString());
        }
        #endregion
    }
}