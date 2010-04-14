using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.themes
{
    /// <summary>
    /// Thrown when theme creates problem
    /// </summary>
    internal class ThemeException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">error message</param>
        public ThemeException(string message)
            : base(message)
        {
        }
    }
}
