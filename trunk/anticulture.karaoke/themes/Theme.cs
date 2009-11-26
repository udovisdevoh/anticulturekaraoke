using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.themes
{
    /// <summary>
    /// Theme (lexical field)
    /// </summary>
    public class Theme : ICollection<string>
    {
        #region Fields
        /// <summary>
        /// Theme's name
        /// </summary>
        private string name;

        /// <summary>
        /// Word list
        /// </summary>
        private HashSet<string> wordList = new HashSet<string>();
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

        #region Properties
        /// <summary>
        /// Theme's name
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        #endregion

        #region ICollection<string> Members
        public void Add(string item)
        {
            wordList.Add(item);
        }

        public void Clear()
        {
            wordList.Clear();
        }

        public bool Contains(string item)
        {
            return wordList.Contains(item);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            wordList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return wordList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(string item)
        {
            return wordList.Remove(item);
        }
        #endregion

        #region IEnumerable<string> Members
        public IEnumerator<string> GetEnumerator()
        {
            return wordList.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return wordList.GetEnumerator();
        }
        #endregion
    }
}
