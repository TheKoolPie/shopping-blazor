using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Shopping.Shared.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException()
        {
        }
        public ItemNotFoundException(Type itemType, string id)
            : base($"Item of type '{itemType.Name}' with id '{id}' not found")
        {

        }
        public ItemNotFoundException(string message) : base(message)
        {
        }

        public ItemNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ItemNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
