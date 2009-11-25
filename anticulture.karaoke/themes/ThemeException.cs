using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace anticulture.karaoke.themes
{
    class ThemeException : Exception
    {
        public ThemeException(string message)
            : base(message)
        {
        }
    }
}
