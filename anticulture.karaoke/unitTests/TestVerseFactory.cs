using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using anticulture.karaoke.verseFactory;
using anticulture.karaoke.themes;


namespace anticulture.karaoke
{
    static class TestVerseFactory
    {
        public static void Test()
        {
            VerseFactory verseFactory = new VerseFactory();
            verseFactory.ClearCreationMemory();

            TestAlgorythmNewAge(verseFactory, VerseConstructionSettings.AlgorithmWords);
            verseFactory.ClearCreationMemory();

            TestAlgorythm(verseFactory, VerseConstructionSettings.AlgorithmRhyme);
            TestAlgorythm(verseFactory, VerseConstructionSettings.AlgorithmSplice);
            TestAlgorythm(verseFactory, VerseConstructionSettings.AlgorithmInterleavedAnalogy);
            TestAlgorythm(verseFactory, VerseConstructionSettings.AlgorithmAnalogy);
            TestAlgorythm(verseFactory, VerseConstructionSettings.AlgorithmStraight);
            TestAlgorythm(verseFactory, VerseConstructionSettings.AlgorithmCryptic);
        }

        private static void TestAlgorythmNewAge(VerseFactory verseFactory, byte algorithmId)
        {
            ThemeLoader themeLoader = new ThemeLoader();
            verseFactory.AddTheme(themeLoader.Load("newage"));

            Verse verse;

            verseFactory.Algorithm = algorithmId;

            verseFactory.DesiredLength = 32;

            for (int i = 0; i < 4; i++)
            {
                verse = verseFactory.Build();
                Console.WriteLine(verse.ToString());
                if ((i + 1) % 4 == 0)
                    Console.WriteLine("");
            }
        }

        private static void TestAlgorythm(VerseFactory verseFactory, byte algorithmId)
        {
            ThemeLoader themeLoader = new ThemeLoader();
            verseFactory.AddTheme(themeLoader.Load("activism"));
            verseFactory.AddTheme(themeLoader.Load("fantastic"));
            verseFactory.AddTheme(themeLoader.Load("philosophy"));
            verseFactory.AddTheme(themeLoader.Load("geek"));
            verseFactory.AddTheme(themeLoader.Load("metaphysics"));

            Verse verse;

            verseFactory.Algorithm = algorithmId;

            verseFactory.DesiredLength = 32;

            for (int i = 0; i < 4; i++)
            {
                verse = verseFactory.Build();
                Console.WriteLine(verse.ToString());
                if ((i + 1) % 4 == 0)
                    Console.WriteLine("");
            }
        }
    }
}
