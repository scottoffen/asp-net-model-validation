using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Wsr.ModelValidation.Exceptions
{
    [System.Serializable]
    public class ModelValidationException : System.Exception
    {
        private static readonly string defaultErrorMessage = "Model validation failed.";

        public ModelStateDictionary ModelState { get; set; }

        public ModelValidationException(ModelStateDictionary modelState) : this(defaultErrorMessage, modelState) { }

        public ModelValidationException(string message, ModelStateDictionary modelState) : base(message)
        {
            this.ModelState = modelState;
        }

        public ModelValidationException(string message, System.Exception inner) : base(message, inner) { }

        protected ModelValidationException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}