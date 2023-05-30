namespace SampleApi.FluentValidationManual.Model
{
    public class SampleRequest
    {
        public bool AutoRedirect { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }
    }
}