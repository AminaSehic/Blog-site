using System;
using System.Runtime.Serialization;

namespace BlogApiRubicon.Controllers
{
    [Serializable]
    internal class MyNotFoundException : Exception
    {
        public MyNotFoundException()
        {
        }

        public MyNotFoundException(string message) : base(message)
        {
        }

        public MyNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}