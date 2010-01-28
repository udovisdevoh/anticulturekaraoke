using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using anticulture.karaoke.themes;

namespace anticulture.karaoke.verseFactory
{
    class VerseFactoryWords : AbstractVerseFactory
    {
        #region Fields
        /// <summary>
        /// Verse construction settings
        /// </summary>
        protected VerseConstructionSettings verseConstructionSettings;

        /// <summary>
        /// Creation memory
        /// </summary>
        protected CreationMemory creationMemory;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="verseConstructionSettings">verse construction settings</param>
        public VerseFactoryWords(VerseConstructionSettings verseConstructionSettings, CreationMemory creationMemory)
        {
            this.verseConstructionSettings = verseConstructionSettings;
            this.creationMemory = creationMemory;
        }
        #endregion

        #region Public Methods
        public override Verse Build(Verse previousVerse)
        {
            string verseContent = string.Empty;
            Theme currentTheme;
            string currentWord;

            while (verseContent.Length < verseConstructionSettings.DesiredLength - 4)
            {
                currentTheme = verseConstructionSettings.ThemeList.GetRandomTheme(verseConstructionSettings.Random);
                currentWord = currentTheme.GetRandomWord(verseConstructionSettings.Random);
                verseContent += " " + currentWord;
            }

            verseContent = verseContent.Trim();

            return new Verse(verseContent);
        }
        #endregion
    }
}
