using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.verseFactory
{
    class AnalogyManager
    {
        #region Constants
        private const string semanticMatrixFileName = "trimmedSemanticMatrix.trimmedSemanticMatrix.xml";
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
        /// Tries to find best analogy for word
        /// </summary>
        /// <param name="word">word to get analogy from</param>
        /// <param name="desiredThemeList">desired theme list</param>
        /// <param name="currentThemeNameListForWord">current theme name list for word</param>
        /// <param name="creationMemory">creation memory</param>
        /// <returns>best analogy or null if no analogy found</returns>
        public string TryGetBestAnalogy(string word, ThemeList desiredThemeList, ThemeList currentThemeListForWord, CreationMemory creationMemory, HashSet<string> wordListInOriginalVerse)
        {
            string bestAnalogy = null;
            Dictionary<string,float> row;
            HashSet<string> availableWordListForAnalogy;
            if (semanticLikenessMatrix.NormalData.TryGetValue(word, out row))
            {
                availableWordListForAnalogy = new HashSet<string>(row.Keys);

                foreach (string analogyCandidate in availableWordListForAnalogy)
                {
                    if (desiredThemeList.Contains(analogyCandidate) && !currentThemeListForWord.Contains(analogyCandidate) && !creationMemory.ContainsWord(analogyCandidate) && !wordListInOriginalVerse.Contains(analogyCandidate))
                    {
                        bestAnalogy = analogyCandidate;
                        break;
                    }
                }
            }

            creationMemory.RememberWord(bestAnalogy);

            return bestAnalogy;
        }
        #endregion
    }
}
