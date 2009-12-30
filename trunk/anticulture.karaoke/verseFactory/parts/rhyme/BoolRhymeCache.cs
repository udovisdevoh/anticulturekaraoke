using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.verseFactory
{
    class BoolRhymeCache
    {
        #region Fields
        private Dictionary<string, bool> internalDictionary = new Dictionary<string, bool>();
        #endregion

        #region Public Methods
        public bool ContainsRhymeInfoAbout(string word1, string word2)
        {
            string key = GetKey(word1, word2);
            return internalDictionary.ContainsKey(key);
        }

        public bool IsRhymeWith(string word1, string word2)
        {
            string key = GetKey(word1, word2);
            bool isRhymeWith = false;
            internalDictionary.TryGetValue(key, out isRhymeWith);
            return isRhymeWith;
        }

        public void AddRhymeInfo(string word1, string word2, bool isRhymeWith)
        {
            string key = GetKey(word1, word2);

            if (internalDictionary.ContainsKey(key))
                internalDictionary[key] = isRhymeWith;
            else
                internalDictionary.Add(key, isRhymeWith);
        }
        #endregion

        #region Private Methods
        private string GetKey(string word1, string word2)
        {
            return word1 + " : " + word2;
        }
        #endregion
    }
}
