using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using anticulture.karaoke.verseFactory;

namespace anticulture.karaoke
{
    /// <summary>
    /// Manipulations on strings
    /// </summary>
    static class StringManipulation
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
        /// Clean the string
        /// </summary>
        /// <param name="text">source string</param>
        /// <returns>cleaned string</returns>
        public static string HardTrim(this string text)
        {
            if (text == "[stop]")
                return text;

            while (text.Contains("  "))
                text = text.Replace("  ", " ");
            if (text.EndsWith("[stop]"))
                text = text.Substring(0, text.Length - 6);
            text = text.Trim();
            return text;
        }

        /// <summary>
        /// Clean the verse
        /// </summary>
        /// <param name="verse">source verse</param>
        /// <returns>cleaned verse</returns>
        public static Verse HardTrim(this Verse verse)
        {
            return new Verse(verse.ToString().HardTrim());
        }

        public static string PunctuationToSpace(this string text)
        {
            return notALetterNorSpace.Replace(text, " ");
        }

        public static string ReplaceWord(this string source, string fromWord, string toWord)
        {
            Regex fromWordMatcher = new Regex("([^a-zA-Z])" + fromWord + "([^a-zA-Z])");
            source = " " + source + " ";

            Match match = fromWordMatcher.Match(source);

            if (match != null && match.Value.Length > 2)
            {
                toWord = match.Value.Substring(0,1) + toWord + match.Value.Substring(match.Value.Length - 1, 1);
                source = source.Replace(match.Value, toWord);
            }

            source = source.Trim();
            return source;
        }

        public static bool IsForeignLanguage(this string source)
        {
            foreach (char currentChar in source)
                if (currentChar > '\x00FF')
                    return true;
            
            return false;
        }

        public static string RemoveLastWord(this string source)
        {
            source = source.Trim();
            if (!source.Contains(' '))
                return source;
            
            int lastSpacePosition = source.LastIndexOf(' ');
            return source.Substring(0, lastSpacePosition).Trim();
        }
        #endregion
    }
}
