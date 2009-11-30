using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using anticulture.karaoke.verseFactory;

namespace anticulture.karaoke
{
    /// <summary>
    /// Manipulations on strings
    /// </summary>
    static class StringManipulation
    {
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

        /// <summary>
        /// Receives string and returns the string with its letters reversed.
        /// </summary>
        public static string ReverseString(this string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

    }
}
