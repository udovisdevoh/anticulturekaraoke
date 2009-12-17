﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.verseFactory
{
    class CreationMemory
    {
        #region Fields
        private Dictionary<string, int> occurencePerTheme = new Dictionary<string, int>();

        private HashSet<string> themeWordList = new HashSet<string>();
        #endregion

        #region Public Methods
        /// <summary>
        /// Clear creation memory
        /// </summary>
        public void Clear()
        {
            occurencePerTheme.Clear();
            themeWordList.Clear();
        }

        /// <summary>
        /// Remember information from generated verse to avoid repitition
        /// </summary>
        /// <param name="verse">generated verse</param>
        /// <param name="verseConstructionSettings">verse construction settings</param>
        public void Remember(Verse verse, VerseConstructionSettings verseConstructionSettings)
        {
            themeWordList.UnionWith(Evaluator.GetThemeWords(verse, verseConstructionSettings.ThemeList));
            AddOccurenceCountPerTheme(Evaluator.CountOccurencePerTheme(verse, verseConstructionSettings.ThemeList));
        }

        public bool ContainsWord(string word)
        {
            return themeWordList.Contains(word);
        }

        public int GetThemeAddedValue(string themeName)
        {
            int value;
            if (!occurencePerTheme.TryGetValue(themeName, out value))
                value = 0;

            value = value - GetMinimumlyGeneratedVerseWordCount();

            value = 10 - value;

            if (value < 0)
                value = 0;

            return value;
        }
        #endregion

        #region Private Methods
        private void AddOccurenceCountPerTheme(Dictionary<string, int> localOccurencePerTheme)
        {
            foreach (KeyValuePair<string, int> themeAndCount in localOccurencePerTheme)
            {
                if (!occurencePerTheme.ContainsKey(themeAndCount.Key))
                    occurencePerTheme.Add(themeAndCount.Key, 0);
                occurencePerTheme[themeAndCount.Key] += themeAndCount.Value;
            }
        }

        private int GetMinimumlyGeneratedVerseWordCount()
        {
            int count = -1;

            foreach (int currentCount in occurencePerTheme.Values)
                if (currentCount < count || count == -1)
                    count = currentCount;

            return count;
        }
        #endregion
    }
}
