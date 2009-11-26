using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.verseFactory
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
            while (text.Contains("  "))
                text = text.Replace("  ", " ");
            text = text.Trim();
            return text;
        }
    }
}
