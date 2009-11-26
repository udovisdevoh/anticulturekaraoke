using System;
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
            VerseFactory.AddTheme(ThemeLoader.Load("urban"));
            VerseFactory.AddTheme(ThemeLoader.Load("nature"));
            VerseFactory.AddTheme(ThemeLoader.Load("religion"));

            VerseFactory.DesiredLength = 255;
            VerseFactory.Algorithm = VerseFactory.AlgorithmMarkov;

            Verse verse = VerseFactory.Build();
            Console.WriteLine(verse.ToString());
            Console.ReadLine();
        }
    }
}
