namespace SampleApi.CustomValidation.Models
{
    public class SampleUpdateRequest : SampleCreateRequest
    {
        public Guid Id { get; set; }
    }
}