using System.ComponentModel.DataAnnotations;
using SampleApi.Attributes.Models;

namespace SampleApi.Attributes.Validators
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RequiresPhoneOrEmailAttribute : ValidationAttribute
    {
        private static readonly string[] _memberNames = new[]
        {
            nameof(SampleRequest.Email),
            nameof(SampleRequest.Phone)
        };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = value as SampleRequest;

            if (model == null)
            {
                return ValidationResult.Success;
            }

            if (string.IsNullOrWhiteSpace(model.Email) && string.IsNullOrWhiteSpace(model.Phone))
            {
                return new ValidationResult("A phone number or email address is required", _memberNames);
            }

            return ValidationResult.Success;
        }
    }
}