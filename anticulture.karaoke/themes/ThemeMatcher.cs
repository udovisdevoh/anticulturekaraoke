using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using anticulture.karaoke.verseFactory;

namespace anticulture.karaoke.themes
{
    /// <summary>
    /// Use this to match strings with themes
    /// </summary>
    static class ThemeMatcher
    {
        #region Fields
        /// <summary>
        /// Anything but a letter
        /// </summary>
        private static Regex notALetter = new Regex("[^a-zA-Z]");
        #endregion

        #region Public Methods


        /// <summary>
        /// Get score for a verse according to desired and undesired themes
        /// </summary>
        /// <param name="currentVerse">current verse</param>
        /// <param name="themeList">desired theme list</param>
        /// <param name="blackThemeList">undesired theme list</param>
        /// <returns>score for a verse according to desired and undesired themes</returns>
        public static float GetScore(Verse currentVerse, ThemeList themeList, ThemeList blackThemeList)
        {
            float score = 0.0f;
            score += Match(currentVerse.ToString(), themeList);
            score -= Match(currentVerse.ToString(), blackThemeList);
            return score;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// How mutch the verse line matches provided theme list
        /// </summary>
        /// <param name="verseLine">verse line</param>
        /// <param name="themeList">provided theme list</param>
        /// <returns>how mutch the verse line matches provided theme list</returns>
        private static float Match(string verseLine, ThemeList themeList)
        {
            float match = 0.0f;
            verseLine = notALetter.Replace(verseLine, " ");
            string[] words = verseLine.Split(' ');
            HashSet<string> wordIgnoreList = new HashSet<string>();

            foreach (Theme currentTheme in themeList)
            {
                foreach (string currentWord in words)
                {
                    string word = currentWord.Trim();
                    if (currentTheme.Contains(word) && word.Length > 0 && !wordIgnoreList.Contains(word))
                    {
                        match += 1;
                        wordIgnoreList.Add(word);
                        break;
                    }
                }
            }
            
            return match;
        }
        #endregion
    }
}