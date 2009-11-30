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

            verseFactory.AddTheme(ThemeLoader.Load("urban"));
            verseFactory.AddTheme(ThemeLoader.Load("nature"));

            Verse verse;

            verseFactory.DesiredLength = 32;

            verse = verseFactory.Build();
            Console.WriteLine(verse.ToString());

            verseFactory.Algorithm = VerseFactory.AlgorithmMarkov;
            verse = verseFactory.Build(verse);
            Console.WriteLine(verse.ToString());

            Console.ReadLine();
        }
    }
}
