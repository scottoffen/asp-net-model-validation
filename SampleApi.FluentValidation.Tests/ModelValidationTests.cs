using SampleApi.FluentValidation.Models;
using SampleApi.FluentValidation.Validators;
using FluentValidation.TestHelper;

namespace SampleApi.FluentValidation.Tests;

public class ModelValidationTests
{
    private readonly SampleRequestValidator _sampleRequestValidator = new();

    [Test]
    public void ModelValidation_Passes()
    {
        var model = new SampleRequest
        {
            Email = "user@domain.com",
            Name = Guid.NewGuid().ToString(),
            Phone = "800-867-5309"
        };

        var result = _sampleRequestValidator.TestValidate(model);

        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ModelValidation_Fails_WithoutPhoneOrEmail()
    {
        var model = new SampleRequest
        {
            Name = Guid.NewGuid().ToString(),
        };


        var result = _sampleRequestValidator.TestValidate(model);

        Assert.That(result.IsValid, Is.False);
        Assert.Multiple(() =>
        {
            Assert.That(result.Errors.Count, Is.EqualTo(2));
            Assert.That(result.Errors.Any(x => x.PropertyName == nameof(SampleRequest.Email)), Is.True);
            Assert.That(result.Errors.Any(x => x.PropertyName == nameof(SampleRequest.Phone)), Is.True);
        });
    }

    [Test]
    public void ModelValidation_Fails_WithoutWebsite()
    {
        var model = new SampleRequest
        {
            AutoRedirect = true,
            Email = "user@domain.com",
            Name = Guid.NewGuid().ToString(),
        };

        var result = _sampleRequestValidator.TestValidate(model);

        Assert.That(result.IsValid, Is.False);
        Assert.Multiple(() =>
        {
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0].PropertyName, Is.EqualTo(nameof(SampleRequest.Website)));
        });
    }
}