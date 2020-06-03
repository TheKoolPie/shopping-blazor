using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Shopping.Shared.Exceptions
{
    public class ItemAlreadyExistsException : Exception
    {
        public ItemAlreadyExistsException() { }

        public ItemAlreadyExistsException(Type itemType, string id)
            : base($"Item of type '{itemType.Name}' with id '{id}' already exists")
        {

        }

        public ItemAlreadyExistsException(string message) : base(message)
        {
        }

        public ItemAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ItemAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
