using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using anticulture.karaoke.themes;
using Markov;

namespace anticulture.karaoke.verseFactory
{
    class AnalogyManager
    {
        #region Constants
        private const string semanticMatrixFileName = "textSources/trimmedSemanticMatrix.trimmedSemanticMatrix.xml";
        #endregion

        #region Fields
        private Matrix semanticLikenessMatrix;
        #endregion

        #region Constructor
        public AnalogyManager()
        {
            XmlMatrixSaverLoader xmlMatrixSaverLoader = new XmlMatrixSaverLoader();
            semanticLikenessMatrix = xmlMatrixSaverLoader.Load(semanticMatrixFileName);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Add analogies to verse
        /// </summary>
        /// <param name="verse">original verse</param>
        /// <param name="isUnlimitedReplace">whether we allow unlimited replacement</param>
        /// <param name="creationMemory">creation memory</param>
        /// <param name="verseConstructionSettings">verse construction settings</param>
        /// <returns>verse with new analogies</returns>
        public Verse AddAnalogies(Verse verse, bool isUnlimitedReplace, VerseConstructionSettings verseConstructionSettings, CreationMemory creationMemory)
        {
            string bestAnalogy;
            if (verseConstructionSettings.ThemeList.Count > 0)
            {
                foreach (string word in verse.WordList)
                {
                    bestAnalogy = TryGetBestAnalogy(word, verseConstructionSettings.ThemeList, Evaluator.GetThemeList(word), creationMemory, verse.WordList);
                    if (bestAnalogy != null)
                    {
                        verse = verse.ReplaceWord(word, bestAnalogy);
                        if (!isUnlimitedReplace)
                            return verse;
                    }
                }
            }
            return verse;
        }

        /// <summary>
        /// Tries to find best analogy for word
        /// </summary>
        /// <param name="word">word to get analogy from</param>
        /// <param name="desiredThemeList">desired theme list</param>
        /// <param name="currentThemeNameListForWord">current theme name list for word</param>
        /// <param name="creationMemory">creation memory</param>
        /// <returns>best analogy or null if no analogy found</returns>
        private string TryGetBestAnalogy(string word, ThemeList desiredThemeList, ThemeList currentThemeListForWord, CreationMemory creationMemory, List<string> wordListInOriginalVerse)
        {
            string bestAnalogy = null;
            Dictionary<string,float> row;
            HashSet<string> availableWordListForAnalogy;
            if (semanticLikenessMatrix.NormalData.TryGetValue(word, out row))
            {
                availableWordListForAnalogy = new HashSet<string>(row.Keys);

                foreach (string analogyCandidate in availableWordListForAnalogy)
                {
                    if (desiredThemeList.Contains(analogyCandidate) &&
                        !currentThemeListForWord.Contains(analogyCandidate) &&
                        !creationMemory.ContainsWord(analogyCandidate) &&
                        !wordListInOriginalVerse.Contains(analogyCandidate))
                    {
                        bestAnalogy = analogyCandidate;
                        break;
                    }
                }
            }

            if (bestAnalogy != null)
                creationMemory.RememberWord(bestAnalogy);

            return bestAnalogy;
        }
        #endregion
    }
}
