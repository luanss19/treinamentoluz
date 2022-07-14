using System;
using System.Runtime.Serialization;

namespace Biblioteca_CRUD
{
    [Serializable]
    internal class DatabaseDriverNotImplementedException : Exception
    {
        public DatabaseDriverNotImplementedException()
        {
        }

        public DatabaseDriverNotImplementedException(string message) : base(message)
        {
        }

        public DatabaseDriverNotImplementedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DatabaseDriverNotImplementedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}