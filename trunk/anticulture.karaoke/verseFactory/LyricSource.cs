using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace anticulture.karaoke.verseFactory
{
    /// <summary>
    /// Represents a lyric source
    /// </summary>
    public class LyricSource
    {
        #region Fields
        /// <summary>
        /// File name
        /// </summary>
        private string fileName;

        /// <summary>
        /// Verse list cache
        /// </summary>
        private Dictionary<string, Verse> verseListCache;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a lyric source from file name
        /// </summary>
        /// <param name="fileName">file name</param>
        public LyricSource(string fileName)
        {
            this.fileName = fileName;
            verseListCache = new Dictionary<string, Verse>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Return random source line
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>random source line</returns>
        public Verse GetRandomSourceLine(Random random)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader streamReader = new StreamReader(fileStream);

            string line = string.Empty;
            long position = (long)(random.NextDouble() * fileStream.Length - 300);

            if (position < 0)
                position = 0;

            fileStream.Seek(position, 0);

            line = streamReader.ReadLine();
            line = streamReader.ReadLine();
            line = line.HardTrim();

            Verse verse;

            if (!verseListCache.TryGetValue(line, out verse))
            {
                verse = new Verse(line);
                verseListCache.Add(line, verse);
            }
            return verse;
        }

        /// <summary>
        /// Return random source lines
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="samplingSize">sampling size</param>
        /// <returns>random source lines</returns>
        public IEnumerable<Verse> GetRandomSourceLineList(Random random, int samplingSize)
        {
            HashSet<Verse> verseList = new HashSet<Verse>();
            for (int i = 0; i < samplingSize; i++)
                verseList.Add(GetRandomSourceLine(random));
            return verseList;
        }
        #endregion
    }
}
