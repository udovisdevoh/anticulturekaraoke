using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.verseFactory
{
    /// <summary>
    /// Analogic verse factory
    /// </summary>
    class VerseFactoryAnalogy : AbstractVerseFactory
    {
        #region Fields
        /// <summary>
        /// Verse construction settings
        /// </summary>
        private VerseConstructionSettings verseConstructionSettings;

        private CreationMemory creationMemory;
        #endregion

        #region Constructors
        /// <summary>
        /// Builds analogic verse
        /// </summary>
        /// <param name="verseConstructionSettings">verse construction settings</param>
        public VerseFactoryAnalogy(VerseConstructionSettings verseConstructionSettings, CreationMemory creationMemory)
        {
            this.verseConstructionSettings = verseConstructionSettings;
            this.creationMemory = creationMemory;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Build analogic verse
        /// </summary>
        /// <param name="previousVerse">previous verse</param>
        /// <returns>analogic verse</returns>
        public override Verse Build(Verse previousVerse)
        {
            #warning Implement VerseFactoryAnalogy.Build()

            //creationMemory.Remember(verse, verseConstructionSettings);

            throw new NotImplementedException();
        }
        #endregion
    }
}
