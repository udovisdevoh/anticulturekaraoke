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

        #region Public Methods
        /// <summary>
        /// Get random word from theme
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>random word from theme</returns>
        public string GetRandomWord(Random random)
        {
            int index = random.Next(0, wordList.Count);
            int count = 0;
            foreach (string word in wordList)
            {
                if (count == index)
                {
                    return word;
                }
                count++;
            }
            throw new ThemeException("Theme is empty, cannot pick a random word");
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

        /// <summary>
        /// Add a word to the theme
        /// </summary>
        /// <param name="item">word to add</param>
        public void Add(string item)
        {
            wordList.Add(item);
        }

        /// <summary>
        /// Remove all words from theme
        /// </summary>
        public void Clear()
        {
            wordList.Clear();
        }

        /// <summary>
        /// Whether theme contains word
        /// </summary>
        /// <param name="item">word to check</param>
        /// <returns>Whether theme contains word</returns>
        public bool Contains(string item)
        {
            return wordList.Contains(item);
        }

        /// <summary>
        /// Copy theme to array
        /// </summary>
        /// <param name="array">array</param>
        /// <param name="arrayIndex">array index</param>
        public void CopyTo(string[] array, int arrayIndex)
        {
            wordList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// How many words in theme
        /// </summary>
        public int Count
        {
            get { return wordList.Count; }
        }

        /// <summary>
        /// Whether theme is read only
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Remove word from theme
        /// </summary>
        /// <param name="item">word to remove</param>
        /// <returns>whether it worked</returns>
        public bool Remove(string item)
        {
            return wordList.Remove(item);
        }

        /// <summary>
        /// To iterate in theme
        /// </summary>
        /// <returns>word iterator</returns>
        public IEnumerator<string> GetEnumerator()
        {
            return wordList.GetEnumerator();
        }

        /// <summary>
        /// To iterate in theme
        /// </summary>
        /// <returns>word iterator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return wordList.GetEnumerator();
        }
        #endregion
    }
}
