using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.verseFactory
{
    /// <summary>
    /// Abstract verse factory
    /// </summary>
    public abstract class AbstractVerseFactory
    {
        #region Public Methods
        /// <summary>
        /// Build a verse
        /// </summary>
        /// <param name="previousVerse">previous verse</param>
        /// <returns>verse</returns>
        public abstract Verse Build(Verse previousVerse);

        /// <summary>
        /// Build a verse
        /// </summary>
        /// <returns>verse</returns>
        public Verse Build()
        {
            return Build(null);
        }
        #endregion
    }
}
