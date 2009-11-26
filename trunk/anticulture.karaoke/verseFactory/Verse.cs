﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using anticulture.karaoke.themes;

namespace anticulture.karaoke.verseFactory
{
    /// <summary>
    /// Represents a song's verse
    /// </summary>
    public class Verse
    {
        #region Fields
        /// <summary>
        /// Text value
        /// </summary>
        private string textValue;

        /// <summary>
        /// Score for each theme (don't use directly: lazy initialization)
        /// </summary>
        private Dictionary<string, float> _themeScore;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a verse
        /// </summary>
        /// <param name="textValue">text value</param>
        public Verse(string textValue)
        {
            this.textValue = textValue;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get verse's text value
        /// </summary>
        /// <returns>verse's string value</returns>
        public override string ToString()
        {
            return textValue;
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Score for teach theme
        /// </summary>
        public Dictionary<string, float> ThemeScore
        {
            get
            {
                if (_themeScore == null)
                    _themeScore = new Dictionary<string, float>();
                return _themeScore;
            }
        }
        #endregion
    }
}
