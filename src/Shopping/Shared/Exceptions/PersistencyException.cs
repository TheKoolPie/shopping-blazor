using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Shopping.Shared.Exceptions
{
    public class PersistencyException : Exception
    {
        public PersistencyException()
        {
        }

        public PersistencyException(string message) : base(message)
        {
        }

        public PersistencyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PersistencyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
