using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Wsr.ModelValidation.Exceptions;

namespace Wsr.ModelValidation
{
    public abstract class ModelValidatorBase<T> : IModelValidator<T> where T : class
    {
        public abstract IEnumerable<ValidationResult> Validate(T model, int scenario = 0);

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            return this.Validate(context.ObjectInstance as T);
        }

        public virtual void ValidateAndThrow(T model)
        {
            var results = this.Validate(model);
            if (results.Any())
            {
                var modelState = new ModelStateDictionary();

                foreach (var result in results)
                {
                    if (result.MemberNames.Any())
                    {
                        foreach (var memberName in result.MemberNames)
                        {
                            modelState.AddModelError(memberName, result.ErrorMessage);
                        }
                    }
                    else
                    {
                        modelState.AddModelError(nameof(T), result.ErrorMessage);
                    }
                }

                throw new ModelValidationException(modelState);
            }
        }
    }
}