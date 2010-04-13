using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using anticulture.karaoke.verseFactory;

namespace anticulture.karaoke.themes
{
    /// <summary>
    /// This class is used to load themes from text files
    /// </summary>
    public class ThemeLoader
    {
        #region Fields
        /// <summary>
        /// Theme file name
        /// </summary>
        private string themeFileName = "textSources/themeFile.themes.txt";

        /// <summary>
        /// Theme cache
        /// </summary>
        private Dictionary<string, Theme> themeCache;

        private ThemeList themeList;
        #endregion

        #region Constructor
        /// <summary>
        /// Static constructor, we load the themes from file
        /// </summary>
        public ThemeLoader()
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
        public Theme Load(string themeName)
        {
            Theme theme;
            if (!themeCache.TryGetValue(themeName, out theme))
                throw new ThemeException("Theme not found");
            return theme;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Theme name list
        /// </summary>
        public IEnumerable<string> ThemeNameList
        {
            get{return themeCache.Keys;}
        }

        /// <summary>
        /// Theme list
        /// </summary>
        public ThemeList ThemeList
        {
            get
            {
                if (themeList == null)
                    themeList = new ThemeList(themeCache.Values);

                return themeList;
            }
        }
        #endregion
    }
}
