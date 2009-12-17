using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.verseFactory
{
    /// <summary>
    /// Uses word based markov chains to generate verses
    /// </summary>
    class VerseFactoryMarkov : AbstractVerseFactory
    {
        #region Constants
        /// <summary>
        /// Sampling size
        /// </summary>
        private const int SamplingSize = 200;

        /// <summary>
        /// Minimum count verse ending with stop marker
        /// </summary>
        private const int MinimumCountEndingWithStop = 10;
        #endregion

        #region Fields
        /// <summary>
        /// Verse construction settings
        /// </summary>
        private VerseConstructionSettings verseConstructionSettings;

        private CreationMemory creationMemory;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="verseConstructionSettings">verse construction settings</param>
        public VerseFactoryMarkov(VerseConstructionSettings verseConstructionSettings, CreationMemory creationMemory)
        {
            this.verseConstructionSettings = verseConstructionSettings;
            this.creationMemory = creationMemory;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Uses word based markov chains to generate verses
        /// </summary>
        /// <param name="previousVerse">previous verse</param>
        /// <returns>verse generated using markov chains</returns>
        public override Verse Build(Verse previousVerse)
        {
            IList<Verse> verseList = GetMarkovVerseList(previousVerse, SamplingSize);
            
            verseList = GetVerseEndingWithStop(verseList);
            if (verseList.Count < 1)
                return null;

            Verse verse = Evaluator.PickBestLength(verseList, verseConstructionSettings.DesiredLength);
            verse = verse.HardTrim();

            //if (verse.Length * 1.4 < verseConstructionSettings.DesiredLength || verseConstructionSettings.DesiredLength * 1.4 < verse.Length)
            //    return null;

            creationMemory.Remember(verse, verseConstructionSettings);

            return verse;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get a list of markovly generated verse
        /// </summary>
        /// <param name="previousVerse">previous verse</param>
        /// <param name="samplingSize">sampling size</param>
        /// <returns>list of markovly generated verse</returns>
        private IList<Verse> GetMarkovVerseList(Verse previousVerse, int samplingSize)
        {
            IEnumerable<Verse> verseListStraight;
            IEnumerable<Verse> verseListReversed;

            if (previousVerse == null)
                verseListStraight = VerseConstructionSettings.LyricSource.GetRandomContiguousSourceLineList(VerseConstructionSettings.Random, SamplingSize);
            else
                verseListStraight = VerseConstructionSettings.LyricSource.GetRandomContiguousSourceLineList(VerseConstructionSettings.Random, SamplingSize, previousVerse.ToString(), true);

            verseListReversed = VerseConstructionSettings.LyricSourceReversed.GetRandomContiguousSourceLineList(VerseConstructionSettings.Random, SamplingSize);

            List<Verse> verseList = new List<Verse>();

            for (int i = 0; i < samplingSize; i++)
            {
                WordMatrix wordMatrix = new WordMatrix(verseListStraight, verseListReversed);
                wordMatrix.resetCursor();
                string verseContent = string.Empty;
                string currentWord = null;

                wordMatrix.resetCursor();
                do
                {
                    currentWord = wordMatrix.GenerateNextWord(VerseConstructionSettings.Random);
                    verseContent += " " + currentWord;
                } while (verseContent.Length < verseConstructionSettings.DesiredLength - 5 && currentWord != null);

                verseContent = verseContent.HardTrim();
                verseList.Add(new Verse(verseContent));
            }

            return verseList;
        }

        /// <summary>
        /// Try keep only verses that end with real end
        /// </summary>
        /// <param name="verseList">verse list</param>
        /// <returns>verses that end with real end or just random verse if not possible</returns>
        private IList<Verse> GetVerseEndingWithStop(IList<Verse> verseList)
        {
            List<Verse> listEndingWithStop = new List<Verse>();
            foreach (Verse currentVerse in verseList)
                if (currentVerse.ToString().EndsWith("[stop]"))
                    listEndingWithStop.Add(currentVerse);
            
            return listEndingWithStop;
        }
        #endregion
    }
}
