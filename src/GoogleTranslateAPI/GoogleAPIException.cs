using System;
using System.Runtime.Serialization;

namespace Google.API
{
    public class GoogleAPIException : Exception
    {
        public GoogleAPIException()
        { }

        public GoogleAPIException(string message)
            : base(message)
        { }

        public GoogleAPIException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected GoogleAPIException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
