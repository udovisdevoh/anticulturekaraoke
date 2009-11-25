using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.themes
{
    /// <summary>
    /// Theme (lexical field)
    /// </summary>
    public class Theme
    {
        #region Fields
        /// <summary>
        /// Theme's name
        /// </summary>
        private string name;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a theme
        /// </summary>
        /// <param name="name">theme's name</param>
        public Theme(string name)
        {
            this.name = name;
        }
        #endregion
    }
}
