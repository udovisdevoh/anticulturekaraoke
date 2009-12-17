using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.verseFactory
{
    /// <summary>
    /// Analogic verse factory
    /// </summary>
    class VerseFactoryAnalogy : VerseFactoryStraight
    {
        #region Parts
        private AnalogyManager analogyManager = new AnalogyManager();

        private ThemeList disabledBlackList = new ThemeList();
        #endregion

        #region Constructors
        /// <summary>
        /// Builds analogic verse
        /// </summary>
        /// <param name="verseConstructionSettings">verse construction settings</param>
        public VerseFactoryAnalogy(VerseConstructionSettings verseConstructionSettings, CreationMemory creationMemory) : base(verseConstructionSettings, creationMemory)
        {
            samplingSize = 2000;
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
            ThemeList blackListBackup = verseConstructionSettings.ThemeBlackList;
            verseConstructionSettings.ThemeBlackList = disabledBlackList;

            Verse verse = base.Build(previousVerse);
            verse = AddAnalogies(verse);

            verseConstructionSettings.ThemeBlackList = blackListBackup;

            return verse;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Add analogies to verse
        /// </summary>
        /// <param name="verse">original verse</param>
        /// <returns>verse with new analogies</returns>
        private Verse AddAnalogies(Verse verse)
        {
            string bestAnalogy;
            if (verseConstructionSettings.ThemeList.Count > 0)
            {
                foreach (string word in verse.WordList)
                {
                    bestAnalogy = analogyManager.TryGetBestAnalogy(word, verseConstructionSettings.ThemeList, Evaluator.GetThemeList(word),creationMemory, verse.WordList);
                    if (bestAnalogy != null)
                    {
                        verse = verse.ReplaceWord(word, bestAnalogy);
                        return verse;
                    }
                }
            }
            return verse;
        }
        #endregion
    }
}
