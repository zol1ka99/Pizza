using System;
using System.Runtime.Serialization;

namespace _2019TobbformosMvcPizzaEgyTabla.model
{
    [Serializable]
    internal class ModelPizzaNotValidPhonenumberException : Exception
    {
        public ModelPizzaNotValidPhonenumberException()
        {
        }

        public ModelPizzaNotValidPhonenumberException(string message) : base(message)
        {
        }

        public ModelPizzaNotValidPhonenumberException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ModelPizzaNotValidPhonenumberException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}