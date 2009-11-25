using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using anticulture.karaoke.themes;

namespace anticulture.karaoke.verseFactory
{
    /// <summary>
    /// Produces verses
    /// </summary>
    public static class VerseFactory
    {
        #region Fields and parts
        /// <summary>
        /// Current algorithm
        /// </summary>
        private static byte algorithm;

        /// <summary>
        /// Whether we want to make verse rhyme with previous verse
        /// </summary>
        private static bool endRhyme;

        /// <summary>
        /// Whether we want to make verse begin like previous verse
        /// </summary>
        private static bool startRhyme;

        /// <summary>
        /// Desired length for verse
        /// </summary>
        private static byte desiredLength;

        /// <summary>
        /// Internal theme list, do not use directly : lazy initialization
        /// </summary>
        private static ThemeList _themeList;
        #endregion

        #region Constants
        /// <summary>
        /// Identifies straight algorithm
        /// </summary>
        public const byte AlgorithmStraight = 0;

        /// <summary>
        /// Identifies markov chain algorithm
        /// </summary>
        public const byte AlgorithmMarkov = 1;

        /// <summary>
        /// Default desired length in char
        /// </summary>
        public const byte DefaultDesiredLength = 32;
        #endregion

        #region Constructors
        static VerseFactory()
        {
            ResetToDefaultSettings();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Reset to default settings
        /// </summary>
        public static void ResetToDefaultSettings()
        {
            algorithm = AlgorithmStraight;
            endRhyme = false;
            startRhyme = false;
            desiredLength = DefaultDesiredLength;
            ResetThemes();
        }

        /// <summary>
        /// Reset current selected themes
        /// </summary>
        public static void ResetThemes()
        {
            ThemeList.Clear();
        }

        /// <summary>
        /// Add a theme to current theme manager
        /// </summary>
        /// <param name="theme">theme to add</param>
        public static void AddTheme(Theme theme)
        {
            ThemeList.Add(theme);
        }

        /// <summary>
        /// Build a verse
        /// </summary>
        /// <returns>verse</returns>
        public static Verse Build()
        {
            #warning Implement
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Set which algorithm to use
        /// </summary>
        public static byte Algorithm
        {
            get { return algorithm; }
            set { algorithm = value; }
        }

        /// <summary>
        /// Whether we want to make verse begin like previous verse
        /// </summary>
        public static bool EndRhyme
        {
            get { return endRhyme; }
            set { endRhyme = value; }
        }

        /// <summary>
        /// Desired length in char for verse
        /// </summary>
        public static byte DesiredLength
        {
            get { return desiredLength; }
            set { desiredLength = value; }
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Current theme manager
        /// </summary>
        private static ThemeList ThemeList
        {
            get
            {
                if (_themeList == null)
                    _themeList = new ThemeList();
                return _themeList;
            }
        }
        #endregion
    }
}
