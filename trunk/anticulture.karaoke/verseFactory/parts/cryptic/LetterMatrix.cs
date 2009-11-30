using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.verseFactory
{
    /// <summary>
    /// Letter matrix
    /// </summary>
    class LetterMatrix
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="verseList">Verse list</param>
        public LetterMatrix(IEnumerable<Verse> verseList)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Build a verse from letter markov matrix
        /// </summary>
        /// <param name="desiredLength">Desired length (in chars)</param>
        /// <param name="random">random</param>
        /// <returns>Verse</returns>
        public Verse BuildSentence(short desiredLength, Random random)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
