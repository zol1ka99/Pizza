using System;
using System.Runtime.Serialization;

namespace _2019TobbformosMvcPizzaEgyTabla.model
{
    [Serializable]
    internal class ModelFutarNotValidNameException : Exception
    {
        public ModelFutarNotValidNameException()
        {
        }

        public ModelFutarNotValidNameException(string message) : base(message)
        {
        }

        public ModelFutarNotValidNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ModelFutarNotValidNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}