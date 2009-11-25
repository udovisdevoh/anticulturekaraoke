using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace anticulture.karaoke.themes
{
    /// <summary>
    /// This class is used to read theme lists from file
    /// </summary>
    static class ThemeFileLoader
    {
        #region Public Methods
        /// <summary>
        /// Read theme list from file
        /// </summary>
        /// <param name="themeFileName">theme file name</param>
        /// <returns>theme list</returns>
        public static Dictionary<string, Theme> LoadThemeList(string themeFileName)
        {
            Dictionary<string, Theme> themeList = new Dictionary<string, Theme>();

            String currentThemeName = null;
            StreamReader streamReader = new StreamReader(themeFileName);

            string line = null;
            while (true)
            {
                line = streamReader.ReadLine();
                if (line == null)
                    break;

                currentThemeName = TrySwitchTheme(line, currentThemeName);
                
                if (IsWordList(line) && currentThemeName != null)
                    AddWordListToTheme(ExtractWordList(line), GetOrCreateTheme(currentThemeName, themeList));
            }

            return themeList;
        }

        /// <summary>
        /// Add word list to theme
        /// </summary>
        /// <param name="wordList">word list</param>
        /// <param name="theme">theme</param>
        private static void AddWordListToTheme(IEnumerable<string> wordList, Theme theme)
        {
            foreach (String word in wordList)
                theme.Add(word);
        }

        /// <summary>
        /// Returns theme from name
        /// </summary>
        /// <param name="themeName">theme's name</param>
        /// <param name="themeList">list to look into</param>
        /// <returns>found theme or new theme</returns>
        private static Theme GetOrCreateTheme(String themeName, Dictionary<string, Theme> themeList)
        {
            Theme theme;
            if (!themeList.TryGetValue(themeName, out theme))
            {
                theme = new Theme(themeName);
                themeList.Add(themeName, theme);
            }
            return theme;
        }

        /// <summary>
        /// Whether the line is a list of words for a theme
        /// </summary>
        /// <param name="line">text line</param>
        /// <returns>whether the line is a list of words for a theme</returns>
        private static bool IsWordList(string line)
        {
            return !line.Contains('<');
        }

        /// <summary>
        /// Try to switch to another theme from line
        /// </summary>
        /// <param name="line">line</param>
        /// <param name="currentThemeName">current theme</param>
        /// <returns>old theme or new theme</returns>
        private static string TrySwitchTheme(string line, string currentThemeName)
        {
            line = line.Trim();
            line = line.Replace(" ", "");
            if (!line.StartsWith("<") || line.StartsWith("</"))
                return currentThemeName;
            else
            {
                line = line.Substring(line.IndexOf("\"") + 1);
                line = line.Substring(0, line.IndexOf("\""));
                return line;
            }
        }

        /// <summary>
        /// Extract word list from line
        /// </summary>
        /// <param name="line">line</param>
        /// <returns>word list from line</returns>
        private static IEnumerable<string> ExtractWordList(string line)
        {
            string[] wordArray = line.Split(',');
            List<string> wordList = new List<string>();

            foreach (string currentWord in wordArray)
            {
                string word = currentWord;
                word = word.Trim().ToLower();
                if (word.Length > 0)
                    wordList.Add(word);
            }

            return wordList;
        }
        #endregion
    }
}