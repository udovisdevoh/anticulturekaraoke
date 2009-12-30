using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.verseFactory
{
    class SimilarPhoneticValueCache
    {
        #region Fields
        private Dictionary<string, string> internalDictionary = new Dictionary<string,string>();
        #endregion

        #region Public Methods
        public void Add(string key, string value)
        {
            if (internalDictionary.ContainsKey(key))
            {
                internalDictionary[key] = value;
            }
            else
            {
                internalDictionary.Add(key, value);
            }
        }

        public string TryGetPhoneticValueOf(string key)
        {
            string value = null;
            internalDictionary.TryGetValue(key, out value);
            return value;
        }
        #endregion
    }
}
