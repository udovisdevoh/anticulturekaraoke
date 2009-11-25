using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using anticulture.karaoke.verseFactory;

namespace anticulture.karaoke
{
    static class Program
    {
        static void Main(string[] args)
        {
            Verse verse = VerseFactory.Build();
            Console.WriteLine(verse.ToString());
            Console.ReadLine();
        }
    }
}
