using System.ComponentModel.DataAnnotations;
using SampleApi.Attributes.Validators;

namespace SampleApi.Attributes.Models
{
    [RequiresPhoneOrEmail]
    [ConditionallyRequiresWebsite]
    public class SampleRequest
    {
        public bool AutoRedirect { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Url]
        public string Website { get; set; }
    }
}