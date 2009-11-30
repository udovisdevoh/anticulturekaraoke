using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.verseFactory
{
    class VerseFactoryException : Exception
    {
        public VerseFactoryException(string message)
            : base(message)
        {
        }
    }
}
