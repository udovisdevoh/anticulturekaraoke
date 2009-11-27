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

            Verse verse;

            VerseFactory.DesiredLength = 64;
            
            verse = VerseFactory.Build();
            Console.WriteLine(verse.ToString());

            VerseFactory.Algorithm = VerseFactory.AlgorithmMarkov;
            verse = VerseFactory.Build(verse);
            Console.WriteLine(verse.ToString());

            Console.ReadLine();
        }
    }
}
