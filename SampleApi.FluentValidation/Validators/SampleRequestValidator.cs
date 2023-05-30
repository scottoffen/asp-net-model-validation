using System.ComponentModel.DataAnnotations;
using FluentValidation;
using SampleApi.FluentValidation.Models;

namespace SampleApi.FluentValidation.Validators
{
    public class SampleRequestValidator : AbstractValidator<SampleRequest>
    {
        private static readonly PhoneAttribute _phoneValidator = new();
        private static readonly UrlAttribute _urlValidator = new();

        public SampleRequestValidator()
        {
            RuleFor(request => request.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MinimumLength(3);

            When(request => !string.IsNullOrWhiteSpace(request.Email), () =>
            {
                RuleFor(request => request.Email)
                    .EmailAddress()
                    .WithMessage("Invalid email address");
            })
            .Otherwise(() =>
            {
                RuleFor(request => request.Phone)
                    .NotEmpty()
                    .WithMessage("A phone number or email address is required");
            });

            When(request => !string.IsNullOrWhiteSpace(request.Phone), () =>
            {
                RuleFor(request => request.Phone)
                    .Must(phone => _phoneValidator.IsValid(phone))
                    .WithMessage("Invalid phone number");
            })
            .Otherwise(() =>
            {
                RuleFor(request => request.Email)
                    .NotEmpty()
                    .WithMessage("A phone number or email address is required");
            });

            When(request => request.AutoRedirect || !string.IsNullOrWhiteSpace(request.Website), () =>
            {
                RuleFor(request => request.Website)
                    .NotEmpty()
                    .When(request => request.AutoRedirect, ApplyConditionTo.CurrentValidator)
                    .WithMessage("Website is a required field to use autoredirect")
                    .Must(website => _urlValidator.IsValid(website))
                    .WithMessage("Invalid website url");
            });
        }
    }
}