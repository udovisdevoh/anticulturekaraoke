using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.verseFactory
{
    class VerseFactorySplice : VerseFactoryStraight
    {
        #region Constructors
        /// <summary>
        /// Builds analogic verse
        /// </summary>
        /// <param name="verseConstructionSettings">verse construction settings</param>
        public VerseFactorySplice(VerseConstructionSettings verseConstructionSettings, CreationMemory creationMemory)
            : base(verseConstructionSettings, creationMemory) {}
        #endregion

        #region Public Methods
        /// <summary>
        /// Build spliced verse
        /// </summary>
        /// <param name="previousVerse">previous verse</param>
        /// <returns>analogic verse</returns>
        public override Verse Build(Verse previousVerse)
        {
            #warning Implement VerseFactorySplice.Build()
            Verse verse = base.Build(previousVerse);
            return verse;
        }
        #endregion
    }
}