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
        private VerseConstructionSettings verseConstructionSettings;
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
            verseConstructionSettings = new VerseConstructionSettings();
            verseFactoryStraight = new VerseFactoryStraight(verseConstructionSettings);
            verseFactoryMarkov = new VerseFactoryMarkov(verseConstructionSettings);
            verseFactoryCryptic = new VerseFactoryCryptic(verseConstructionSettings);
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
            if (verseConstructionSettings.Algorithm == VerseConstructionSettings.AlgorithmStraight)
            {
                verse = verseFactoryStraight.Build(previousVerse);
            }
            else if (verseConstructionSettings.Algorithm == VerseConstructionSettings.AlgorithmMarkov)
            {
                verse = verseFactoryMarkov.Build(previousVerse);
            }
            else if (verseConstructionSettings.Algorithm == VerseConstructionSettings.AlgorithmCryptic)
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

        /// <summary>
        /// Add theme to construction settings
        /// </summary>
        /// <param name="theme">theme to add</param>
        public void AddTheme(Theme theme)
        {
            verseConstructionSettings.AddTheme(theme);
        }

        /// <summary>
        /// Reset themes
        /// </summary>
        public void ResetThemes()
        {
            verseConstructionSettings.ResetThemes();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Desired length in char for verse
        /// </summary>
        public short DesiredLength
        {
            get { return verseConstructionSettings.DesiredLength; }
            set { verseConstructionSettings.DesiredLength = value; }
        }

        /// <summary>
        /// Set which algorithm to use
        /// </summary>
        public byte Algorithm
        {
            get { return verseConstructionSettings.Algorithm; }
            set { verseConstructionSettings.Algorithm = value; }
        }
        #endregion
    }
}