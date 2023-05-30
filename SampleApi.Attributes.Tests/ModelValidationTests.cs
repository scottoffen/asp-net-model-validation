using System.ComponentModel.DataAnnotations;
using SampleApi.Attributes.Models;

namespace SampleApi.Attributes.Tests;

public class ModelValidationTests
{
    [Test]
    public void ModelValidation_Passes()
    {
        var model = new SampleRequest
        {
            Email = "user@domain.com",
            Name = Guid.NewGuid().ToString(),
            Phone = "800-867-5309"
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.That(isValid, Is.True);
    }

    [Test]
    public void ModelValidation_Fails_WithoutPhoneOrEmail()
    {
        var model = new SampleRequest
        {
            Name = Guid.NewGuid().ToString(),
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.That(isValid, Is.False);
        Assert.Multiple(() =>
        {
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].MemberNames.Count, Is.EqualTo(2));
            Assert.That(results[0].MemberNames.Contains(nameof(SampleRequest.Email)), Is.True);
            Assert.That(results[0].MemberNames.Contains(nameof(SampleRequest.Phone)), Is.True);
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

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.That(isValid, Is.False);
        Assert.Multiple(() =>
        {
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].MemberNames.Count, Is.EqualTo(1));
            Assert.That(results[0].MemberNames.Contains(nameof(SampleRequest.Website)), Is.True);
        });
    }
}