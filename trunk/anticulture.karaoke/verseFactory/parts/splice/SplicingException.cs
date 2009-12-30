using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.verseFactory
{
    class SplicingException : Exception
    {
        public SplicingException(string message) : base(message) { }
    }
}
