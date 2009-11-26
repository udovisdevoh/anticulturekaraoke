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
        private static ThemeList themeList = new ThemeList();

        /// <summary>
        /// Internal theme black list, do not use directly : lazy initialization
        /// </summary>
        private static ThemeList themeBlackList = new ThemeList();

        /// <summary>
        /// Straight ordered lyrics
        /// </summary>
        private static LyricSource lyricSource;

        /// <summary>
        /// Reverse ordered lyrics
        /// </summary>
        private static LyricSource lyricSourceReversed;

        /// <summary>
        /// Random number generator
        /// </summary>
        private static Random random;
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

        /// <summary>
        /// Ordered lyrics file name
        /// </summary>
        public const string lyricsFileName = "lyrics.en.txt";

        /// <summary>
        /// Reversed order lyrics file name
        /// </summary>
        public const string reversedLyricsFileName = "lyrics.en.reversed.txt";
        #endregion

        #region Constructors
        static VerseFactory()
        {
            ResetToDefaultSettings();
            lyricSource = new LyricSource(lyricsFileName);
            lyricSourceReversed = new LyricSource(reversedLyricsFileName);
            random = new Random();
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
            themeList.Clear();
            themeBlackList.Clear();
        }

        /// <summary>
        /// Add a theme to current theme list
        /// </summary>
        /// <param name="theme">theme to add</param>
        public static void AddTheme(Theme theme)
        {
            if (themeBlackList.Contains(theme))
                themeBlackList.Remove(theme);
            themeList.Add(theme);
        }

        /// <summary>
        /// Censor a theme 
        /// </summary>
        /// <param name="theme">theme to censor</param>
        public static void CensorTheme(Theme theme)
        {
            if (themeList.Contains(theme))
                themeList.Remove(theme);
            themeBlackList.Add(theme);
        }

        /// <summary>
        /// Build a verse
        /// </summary>
        /// <returns>verse</returns>
        public static Verse Build()
        {
            return Build(null);
        }

        /// <summary>
        /// Build a verse
        /// </summary>
        /// <returns>verse</returns>
        public static Verse Build(Verse previousVerse)
        {
            switch (algorithm)
            {
                case AlgorithmStraight:
                    return VerseFactoryStraight.Build(previousVerse);
                case AlgorithmMarkov:
                    return VerseFactoryMarkov.Build(previousVerse);
                default:
                    return VerseFactoryStraight.Build(previousVerse);
            }
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

        /// <summary>
        /// Ordered lyric source
        /// </summary>
        public static LyricSource LyricSource
        {
            get { return lyricSource; }
        }

        /// <summary>
        /// Reverse order lyric source
        /// </summary>
        public static LyricSource LyricSourceReversed
        {
            get { return lyricSource; }
        }

        /// <summary>
        /// Random number generator
        /// </summary>
        public static Random Random
        {
            get { return random; }
        }

        /// <summary>
        /// Desired themes
        /// </summary>
        public static ThemeList ThemeList
        {
            get { return themeList; }
        }

        /// <summary>
        /// Undesired themes
        /// </summary>
        public static ThemeList ThemeBlackList
        {
            get { return themeBlackList; }
        }
        #endregion
    }
}