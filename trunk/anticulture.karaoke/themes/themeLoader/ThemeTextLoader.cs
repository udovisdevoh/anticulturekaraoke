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

                if (IsThemeName(line))
                    currentThemeName = ExtractThemeName(line);
                else if (IsWordList(line) && currentThemeName != null)
                    AddWordListToTheme(ExtractWordList(line), GetOrCreateTheme(themeName,themeList));
            }
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

        private static bool IsWordList(string line)
        {
            throw new NotImplementedException();
        }

        private static bool IsThemeName(string line)
        {
            throw new NotImplementedException();
        }

        private static string ExtractThemeName(string line)
        {
            throw new NotImplementedException();
        }

        private static void AddWordListToTheme(IEnumerable<string> iEnumerable, Theme currentTheme)
        {
            throw new NotImplementedException();
        }

        private static IEnumerable<string> ExtractWordList(string line)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
