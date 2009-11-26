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
        #endregion

        #region Public Methods
        /// <summary>
        /// Uses word based markov chains to generate verses
        /// </summary>
        /// <param name="previousVerse">previous verse</param>
        /// <returns>verse generated using markov chains</returns>
        public static Verse Build(Verse previousVerse)
        {
            IEnumerable<Verse> verseListStraight = VerseFactory.LyricSource.GetRandomContiguousSourceLineList(VerseFactory.Random, SamplingSize);
            IEnumerable<Verse> verseListReversed = VerseFactory.LyricSourceReversed.GetRandomContiguousSourceLineList(VerseFactory.Random, SamplingSize);

            WordMatrix wordMatrix = new WordMatrix(verseListStraight, verseListReversed);
            wordMatrix.resetCursor();
            string verseContent = string.Empty;
            string currentWord = null;

            do
            {
                currentWord = wordMatrix.GenerateNextWord(VerseFactory.Random);
                verseContent += " " + currentWord;
            } while (verseContent.Length < VerseFactory.DesiredLength - 5 && currentWord != null) ;

            verseContent = verseContent.HardTrim();

            return new Verse(verseContent);
        }
        #endregion
    }
}
