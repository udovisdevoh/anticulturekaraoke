using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
            verseLine = notALetter.Replace(verseLine, "");
            string[] words = verseLine.Split(' ');

            foreach (string word in words)
            {
                if (theme.Contains(word))
                {
                    match+= incrementor;
                    incrementor/=3.0f;
                }
            }
            return match;
        }
        #endregion
    }
}