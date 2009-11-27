using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.verseFactory
{
    /// <summary>
    /// Uses word based markov chains to generate verses
    /// </summary>
    class VerseFactoryMarkov
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

        #region Public Methods
        /// <summary>
        /// Uses word based markov chains to generate verses
        /// </summary>
        /// <param name="previousVerse">previous verse</param>
        /// <returns>verse generated using markov chains</returns>
        public static Verse Build(Verse previousVerse)
        {
            IList<Verse> verseList = GetMarkovVerseList(previousVerse, SamplingSize);
            
            verseList = GetVerseEndingWithStop(verseList);
            if (verseList.Count < 1)
                return VerseFactoryStraight.Build(previousVerse);

            Verse verse = Evaluator.PickBestLength(verseList, VerseFactory.DesiredLength);
            verse = verse.HardTrim();
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
        private static IList<Verse> GetMarkovVerseList(Verse previousVerse, int samplingSize)
        {
            IEnumerable<Verse> verseListStraight;
            IEnumerable<Verse> verseListReversed;

            if (previousVerse == null)
            {
                verseListStraight = VerseFactory.LyricSource.GetRandomContiguousSourceLineList(VerseFactory.Random, SamplingSize);
                verseListReversed = VerseFactory.LyricSourceReversed.GetRandomContiguousSourceLineList(VerseFactory.Random, SamplingSize);
            }
            else
            {
                //verseListStraight = VerseFactory.LyricSource.GetRandomContiguousSourceLineList(VerseFactory.Random, SamplingSize, previousVerse.ToString(), true);
                //verseListReversed = VerseFactory.LyricSourceReversed.GetRandomContiguousSourceLineList(VerseFactory.Random, SamplingSize);
                if (VerseFactory.Random.Next(0, 2) == 1)
                {
                    verseListStraight = VerseFactory.LyricSource.GetRandomContiguousSourceLineList(VerseFactory.Random, SamplingSize,previousVerse.ToString(),true);
                    verseListReversed = VerseFactory.LyricSourceReversed.GetRandomContiguousSourceLineList(VerseFactory.Random, SamplingSize);
                }
                else
                {
                    verseListStraight = VerseFactory.LyricSource.GetRandomContiguousSourceLineList(VerseFactory.Random, SamplingSize);
                    verseListReversed = VerseFactory.LyricSourceReversed.GetRandomContiguousSourceLineList(VerseFactory.Random, SamplingSize, previousVerse.ToString(), false);
                }
            }

            List<Verse> verseList = new List<Verse>();

            for (int i = 0; i < samplingSize; i++)
            {
                WordMatrix wordMatrix = new WordMatrix(verseListStraight, verseListReversed);
                wordMatrix.resetCursor();
                string verseContent = string.Empty;
                string currentWord = null;

                do
                {
                    currentWord = wordMatrix.GenerateNextWord(VerseFactory.Random);
                    verseContent += " " + currentWord;
                } while (verseContent.Length < VerseFactory.DesiredLength - 5 && currentWord != null);

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
        private static IList<Verse> GetVerseEndingWithStop(IList<Verse> verseList)
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
