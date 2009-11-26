using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using anticulture.karaoke.themes;

namespace anticulture.karaoke.verseFactory
{
    /// <summary>
    /// Represents a set of themes
    /// </summary>
    public class ThemeList : ICollection<Theme>
    {
        #region Fields
        /// <summary>
        /// Internal references to themes
        /// </summary>
        private HashSet<Theme> themeList = new HashSet<Theme>();
        #endregion

        #region ICollection<Theme> Members
        /// <summary>
        /// Add a theme
        /// </summary>
        /// <param name="theme">theme to add</param>
        public void Add(Theme theme)
        {
            themeList.Add(theme);
        }

        /// <summary>
        /// Clear theme list
        /// </summary>
        public void Clear()
        {
            themeList.Clear();
        }

        /// <summary>
        /// Whether the theme manager contains theme
        /// </summary>
        /// <param name="theme">theme</param>
        /// <returns>whether the theme manager contains theme</returns>
        public bool Contains(Theme theme)
        {
            return themeList.Contains(theme);
        }

        /// <summary>
        /// Copy theme list to array
        /// </summary>
        /// <param name="array">array</param>
        /// <param name="arrayIndex">array index</param>
        public void CopyTo(Theme[] array, int arrayIndex)
        {
            themeList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Count how many themes in theme manager
        /// </summary>
        public int Count
        {
            get { return themeList.Count; }
        }

        /// <summary>
        /// Whether theme manager is readonly
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Remove theme
        /// </summary>
        /// <param name="theme">theme to remove</param>
        /// <returns>if removal succeeded</returns>
        public bool Remove(Theme theme)
        {
            return themeList.Remove(theme);
        }
        #endregion

        #region IEnumerable<Theme> Members
        /// <summary>
        /// Enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<Theme> GetEnumerator()
        {
            return themeList.GetEnumerator();
        }
        #endregion

        #region IEnumerable Members
        /// <summary>
        /// Enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return themeList.GetEnumerator();
        }
        #endregion
    }
}
