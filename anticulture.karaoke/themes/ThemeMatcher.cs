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
        /// Match a verse line to a theme
        /// </summary>
        /// <param name="verseLine">verse line</param>
        /// <param name="themeName">theme name</param>
        /// <returns>float from 0 to something big</returns>
        public static float Match(string verseLine, string themeName)
        {
            Theme theme = ThemeLoader.Load(themeName);
            float match = 0.0f;
            float incrementor = 1.0f;
            verseLine = notALetter.Replace(verseLine, " ");
            string[] words = verseLine.Split(' ');

            foreach (string currentWord in words)
            {
                string word = currentWord.Trim();
                if (theme.Contains(currentWord) && currentWord.Length > 0)
                {
                    match+= incrementor;
                    incrementor/=3.0f;
                }
            }
            return match;
        }

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

            foreach (Theme theme in themeList)
                score += ThemeMatcher.Match(currentVerse.ToString(), theme.Name);

            foreach (Theme theme in blackThemeList)
                score += ThemeMatcher.Match(currentVerse.ToString(), theme.Name);

            return score;
        }
        #endregion
    }
}