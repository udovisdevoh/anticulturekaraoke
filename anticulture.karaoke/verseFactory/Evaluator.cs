using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using anticulture.karaoke.verseFactory;
using anticulture.karaoke.themes;

namespace anticulture.karaoke.verseFactory
{
    /// <summary>
    /// Use this to match strings with themes
    /// </summary>
    static class Evaluator
    {
        #region Fields
        /// <summary>
        /// Anything but a letter
        /// </summary>
        private static Regex notALetter = new Regex(@"[^a-zA-Z]");

        /// <summary>
        /// Anything but a letter or space
        /// </summary>
        private static Regex notALetterNorSpace = new Regex(@"[^a-zA-Z ]");
        #endregion

        #region Public Methods
        /// <summary>
        /// Get score for a verse according to desired and undesired themes
        /// </summary>
        /// <param name="currentVerse">current verse</param>
        /// <param name="themeList">desired theme list</param>
        /// <param name="blackThemeList">undesired theme list</param>
        /// <param name="desiredLength">desired length</param>
        /// <returns>score for a verse according to desired and undesired themes</returns>
        public static int GetScore(Verse currentVerse, ThemeList themeList, ThemeList blackThemeList, short desiredLength)
        {
            int score = 0;
            score += Match(currentVerse.ToString(), themeList);
            score -= Match(currentVerse.ToString(), blackThemeList);

            score = score - Math.Abs(notALetterNorSpace.Replace(currentVerse.ToString(), "").Length - desiredLength);

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
        private static int Match(string verseLine, ThemeList themeList)
        {
            int match = 0;
            string[] words = verseLine.Split(' ');
            HashSet<string> wordIgnoreList = new HashSet<string>();

            foreach (Theme currentTheme in themeList)
            {
                foreach (string currentWord in words)
                {
                    string word = currentWord.Trim();
                    if (currentTheme.Contains(word) && word.Length > 0 && !wordIgnoreList.Contains(word))
                    {
                        match += 10;
                        wordIgnoreList.Add(word);
                        break;
                    }
                }
            }
            
            return match;
        }

        /// <summary>
        /// From verses, pick the one with length closest to desired length
        /// </summary>
        /// <param name="verseList">verse list</param>
        /// <param name="desiredLength">desired length</param>
        /// <returns>from verses, pick the one with length closest to desired length</returns>
        public static Verse PickBestLength(IList<Verse> verseList, int desiredLength)
        {
            Verse bestVerse = null;
            int bestDifference = -1;
            int currentDifference = -1;
            string verseLine;

            foreach (Verse currentVerse in verseList)
            {
                verseLine = currentVerse.ToString().HardTrim();
                currentDifference = Math.Abs(verseLine.Length - desiredLength);

                if (currentDifference < bestDifference || bestVerse == null)
                {
                    bestVerse = currentVerse;
                    bestDifference = currentDifference;
                }
            }

            return bestVerse;
        }
        #endregion
    }
}