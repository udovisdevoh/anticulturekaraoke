using System;
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
        #region Fields
        /// <summary>
        /// Verse construction settings
        /// </summary>
        private VerseConstructionSettings verseConstructionSettings;
        #endregion

        #region Public Methods
        /// <summary>
        /// Generates char based markov chain based verse
        /// </summary>
        /// <param name="previousVerse">previous verse</param>
        /// <returns>char based markov chain based verse</returns>
        public override Verse Build(Verse previousVerse)
        {
            throw new NotImplementedException();
        }
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
    }
}