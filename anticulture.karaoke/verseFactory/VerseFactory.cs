using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using anticulture.karaoke.themes;

namespace anticulture.karaoke.verseFactory
{
    /// <summary>
    /// Produces verses
    /// </summary>
    public class VerseFactory : AbstractVerseFactory
    {
        #region Fields
        /// <summary>
        /// Construction settings
        /// </summary>
        private VerseConstructionSettings constructionSettings;
        #endregion

        #region Parts
        /// <summary>
        /// Straight verse factory
        /// </summary>
        private VerseFactoryStraight verseFactoryStraight;

        /// <summary>
        /// Markov verse factory
        /// </summary>
        private VerseFactoryMarkov verseFactoryMarkov;

        /// <summary>
        /// Cryptic verse factory
        /// </summary>
        private VerseFactoryCryptic verseFactoryCryptic;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public VerseFactory()
        {
            constructionSettings = new VerseConstructionSettings();
            verseFactoryStraight = new VerseFactoryStraight(constructionSettings);
            verseFactoryMarkov = new VerseFactoryMarkov(constructionSettings);
            verseFactoryCryptic = new VerseFactoryCryptic(constructionSettings);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Build a verse
        /// </summary>
        /// <returns>verse</returns>
        public override Verse Build(Verse previousVerse)
        {
            Verse verse;
            if (constructionSettings.Algorithm == AlgorithmStraight)
            {
                verse = verseFactoryStraight.Build(previousVerse);
            }
            else if (constructionSettings.Algorithm == AlgorithmMarkov)
            {
                verse = verseFactoryMarkov.Build(previousVerse);
            }
            else if (constructionSettings.Algorithm == AlgorithmCryptic)
            {
                verse = verseFactoryCryptic.Build(previousVerse);
            }
            else
            {
                throw new VerseFactoryException("Couldn't find proper algorithm to generate verse");
            }

            if (verse == null)
            {
                verse = verseFactoryStraight.Build(previousVerse);
            }

            return verse;
        }
        #endregion
    }
}