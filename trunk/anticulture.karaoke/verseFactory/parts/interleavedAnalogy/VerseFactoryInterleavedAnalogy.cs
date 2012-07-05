﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.verseFactory
{
    class VerseFactoryInterleavedAnalogy : VerseFactoryStraight
    {
        #region Parts
        private AnalogyManager analogyManager = new AnalogyManager();

        private ThemeList disabledBlackList = new ThemeList();
        #endregion

        #region Constructors
        /// <summary>
        /// Builds analogic verse
        /// </summary>
        /// <param name="verseConstructionSettings">verse construction settings</param>
        public VerseFactoryInterleavedAnalogy(VerseConstructionSettings verseConstructionSettings, CreationMemory creationMemory)
            : base(verseConstructionSettings, creationMemory)
        {
            samplingSize = 2000;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Build interleaved analogic verse
        /// </summary>
        /// <param name="previousVerse">previous verse</param>
        /// <returns>analogic verse</returns>
        public override Verse Build(Verse previousVerse)
        {
            ThemeList blackListBackup;

            Verse verse = null;
            Verse verseToRhymeFrom = null;

            if (creationMemory.RhymeSpan == -1)
                creationMemory.RhymeSpan = verseConstructionSettings.GenerateRandomRhymeSpan();

            if (IsMatchRyhmePattern(creationMemory.RhymeSpan,creationMemory.RhymeCounter))
            {
                verseToRhymeFrom = creationMemory.GetVerseToAddRhyme(-creationMemory.RhymeSpan);
                if (verseToRhymeFrom != null)
                {
                    blackListBackup = verseConstructionSettings.ThemeBlackList;
                    verseConstructionSettings.ThemeBlackList = disabledBlackList;
                    verse = analogyManager.AddAnalogies(verseToRhymeFrom, true, verseConstructionSettings, creationMemory);
                    verseConstructionSettings.ThemeBlackList = blackListBackup;
                    if (verse.Equals(verseToRhymeFrom))
                        verse = null;
                }
            }

            if (verse == null)
                verse = base.Build(previousVerse);

            creationMemory.AddVerseToAddRhyme(verse);
            creationMemory.RhymeCounter++;


            return verse;
        }

        private bool IsMatchRyhmePattern(int span, int counter)
        {
            if (counter % (span * 2) >= span)
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}