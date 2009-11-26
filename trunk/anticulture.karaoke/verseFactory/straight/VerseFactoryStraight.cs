using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.verseFactory
{
    class VerseFactoryStraight
    {
        /// <summary>
        /// Build a straight verse
        /// </summary>
        /// <param name="previousVerse">previous verse</param>
        /// <returns>straight verse</returns>
        public static Verse Build(Verse previousVerse)
        {
            Verse sourceLine = VerseFactory.LyricSource.GetRandomSourceLine(VerseFactory.Random);
            return sourceLine;
        }
    }
}
