using System.ComponentModel.DataAnnotations;
using SampleApi.Attributes.Models;

namespace SampleApi.Attributes.Validators
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConditionallyRequiresWebsite : ValidationAttribute
    {
       private static readonly string[] _memberNames = new[]
        {
            nameof(SampleRequest.Website)
        };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = value as SampleRequest;

            if (model == null)
            {
                return ValidationResult.Success;
            }

            if (model.AutoRedirect && string.IsNullOrEmpty(model.Website))
            {
                return new ValidationResult("Website is required to use auto redirect", _memberNames);
            }

            return ValidationResult.Success;
        }
    }
}