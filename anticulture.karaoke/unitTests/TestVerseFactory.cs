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

            TestAlgorythm(verseFactory, VerseConstructionSettings.AlgorithmSplice);
            TestAlgorythm(verseFactory, VerseConstructionSettings.AlgorithmInterleavedAnalogy);
            TestAlgorythm(verseFactory, VerseConstructionSettings.AlgorithmAnalogy);
            TestAlgorythm(verseFactory, VerseConstructionSettings.AlgorithmStraight);
            TestAlgorythm(verseFactory, VerseConstructionSettings.AlgorithmCryptic);

        }

        private static void TestAlgorythm(VerseFactory verseFactory, byte algorithmId)
        {
            verseFactory.AddTheme(ThemeLoader.Load("activism"));
            verseFactory.AddTheme(ThemeLoader.Load("fantastic"));
            verseFactory.AddTheme(ThemeLoader.Load("philosophy"));
            verseFactory.AddTheme(ThemeLoader.Load("geek"));
            verseFactory.AddTheme(ThemeLoader.Load("metaphysics"));

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
