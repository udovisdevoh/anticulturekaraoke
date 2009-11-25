using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.themes
{
    /// <summary>
    /// This class is used to load themes from text files
    /// </summary>
    public static class ThemeLoader
    {
        #region Fields
        /// <summary>
        /// Theme file name
        /// </summary>
        private static string themeFileName = "themes.txt";

        /// <summary>
        /// Theme cache
        /// </summary>
        private static Dictionary<string, Theme> themeCache;
        #endregion

        #region Constructor
        /// <summary>
        /// Static constructor, we load the themes from file
        /// </summary>
        static ThemeLoader()
        {
            themeCache = ThemeFileLoader.LoadThemeList(themeFileName);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Load theme
        /// </summary>
        /// <param name="themeName">theme's name</param>
        /// <returns>loaded theme</returns>
        public static Theme Load(string themeName)
        {
            Theme theme;
            if (!themeCache.TryGetValue(themeName, out theme))
                throw new ThemeException("Theme not found");
            return theme;
        }
        #endregion
    }
}
