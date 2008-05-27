using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Google.API.Translate
{
    public class TranslateException : Exception
    {
        public TranslateException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public TranslateException(string message)
            : base(message)
        { }

        public TranslateException()
        { }
    }
}
