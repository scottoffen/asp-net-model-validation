using System.ComponentModel.DataAnnotations;
using SampleApi.CustomValidation.Models;
using Wsr.ModelValidation;

namespace SampleApi.CustomValidation.Validators
{
    public class SampleUpdateRequestValidator : ModelValidatorBase<SampleUpdateRequest>
    {
        private readonly IModelValidator<SampleCreateRequest> _createRequestValidator;

        public SampleUpdateRequestValidator(IModelValidator<SampleCreateRequest> createRequestValidator)
        {
            this._createRequestValidator = createRequestValidator;
        }

        public override IEnumerable<ValidationResult> Validate(SampleUpdateRequest model, int scenario = 0)
        {
            if (model.Id == Guid.Empty)
            {
                yield return new ValidationResult("Id is a required field", new[] { nameof(SampleUpdateRequest.Id) });
            }

            foreach (var result in this._createRequestValidator.Validate(model))
            {
                yield return result;
            }
        }
    }
}