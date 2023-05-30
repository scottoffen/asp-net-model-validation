using System.ComponentModel.DataAnnotations;

namespace SampleApi.ValidatableObject.Models
{
    public class SampleRequest : IValidatableObject
    {
        private static readonly EmailAddressAttribute _emailValidator = new();
        private static readonly PhoneAttribute _phoneValidator = new();
        private static readonly UrlAttribute _urlValidator = new();

        public bool AutoRedirect { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var allFieldsValidated = true;
            var hasPhoneOrEmail = !(string.IsNullOrWhiteSpace(this.Email) && string.IsNullOrWhiteSpace(this.Phone));
            var hasWebsite = string.IsNullOrWhiteSpace(this.Website);

            if (string.IsNullOrWhiteSpace(this.Name))
            {
                allFieldsValidated = false;
                yield return new ValidationResult("Name is a required value", new[] { nameof(SampleRequest.Name) });
            }

            if (!string.IsNullOrWhiteSpace(this.Email) && !_emailValidator.IsValid(this.Email))
            {
                allFieldsValidated = false;
                yield return new ValidationResult("Not a valid email address", new[] { nameof(SampleRequest.Email) });
            }

            if (!string.IsNullOrWhiteSpace(this.Phone) && !_phoneValidator.IsValid(this.Phone))
            {
                allFieldsValidated = false;
                yield return new ValidationResult("Not a valid phone number", new[] { nameof(SampleRequest.Phone) });
            }

            if (!string.IsNullOrWhiteSpace(this.Website) && !_urlValidator.IsValid(this.Website))
            {
                allFieldsValidated = false;
                yield return new ValidationResult("Invalid website url", new[] { nameof(SampleRequest.Website) });
            }

            if (allFieldsValidated)
            {
                if (!hasPhoneOrEmail)
                {
                    yield return new ValidationResult("Either an email address or a phone number is required", new[] { nameof(SampleRequest.Email), nameof(SampleRequest.Phone) });
                }

                if (this.AutoRedirect && !hasWebsite)
                {
                    yield return new ValidationResult("A website url is required to use auto redirect", new[] { nameof(SampleRequest.Website) });
                }
            }
        }
    }
}