﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using anticulture.karaoke.verseFactory;
using anticulture.karaoke.themes;

namespace anticulture.karaoke
{
    static class Program
    {
        static void Main(string[] args)
        {
            VerseFactory verseFactory = new VerseFactory();

            //verseFactory.AddTheme(ThemeLoader.Load("urban"));
            //verseFactory.AddTheme(ThemeLoader.Load("nature"));
            //verseFactory.AddTheme(ThemeLoader.Load("food"));
            verseFactory.AddTheme(ThemeLoader.Load("activism"));
            verseFactory.AddTheme(ThemeLoader.Load("fantastic"));
            verseFactory.AddTheme(ThemeLoader.Load("philosophy"));
            verseFactory.AddTheme(ThemeLoader.Load("geek"));
            verseFactory.AddTheme(ThemeLoader.Load("metaphysics"));
            //verseFactory.AddTheme(ThemeLoader.Load("food"));
            //verseFactory.AddTheme(ThemeLoader.Load("religion"));
            //verseFactory.AddTheme(ThemeLoader.Load("money"));
            //verseFactory.AddTheme(ThemeLoader.Load("gore"));
            //verseFactory.AddTheme(ThemeLoader.Load("gangsta"));
            //verseFactory.AddTheme(ThemeLoader.Load("shame"));

            Verse verse;

            verseFactory.ClearCreationMemory();

            //verseFactory.Algorithm = VerseConstructionSettings.AlgorithmAnalogy;
            verseFactory.Algorithm = VerseConstructionSettings.AlgorithmInterleavedAnalogy;
            //verseFactory.Algorithm = VerseConstructionSettings.AlgorithmStraight;

            verseFactory.DesiredLength = 32;

            for (int i = 0; i < 16; i++)
            {
                verse = verseFactory.Build();
                Console.WriteLine(verse.ToString());
                if ((i + 1) % 4 == 0)
                    Console.WriteLine("");
            }

            /*verseFactory.Algorithm = VerseConstructionSettings.AlgorithmAnalogy;

            verse = verseFactory.Build();
            Console.WriteLine(verse.ToString());
            verse = verseFactory.Build();
            Console.WriteLine(verse.ToString());
            verse = verseFactory.Build();
            Console.WriteLine(verse.ToString());*/

            //verseFactory.ClearCreationMemory();

            //verse = verseFactory.Build();
            //Console.WriteLine(verse.ToString());

            /*verseFactory.Algorithm = VerseConstructionSettings.AlgorithmMarkov;
            verse = verseFactory.Build(verse);
            Console.WriteLine(verse.ToString());

            verseFactory.Algorithm = VerseConstructionSettings.AlgorithmCryptic;
            verse = verseFactory.Build(verse);
            Console.WriteLine(verse.ToString());

            verseFactory.Algorithm = VerseConstructionSettings.AlgorithmMarkov;
            verse = verseFactory.Build(verse);
            Console.WriteLine(verse.ToString());*/


            Console.ReadLine();
        }
    }
}
