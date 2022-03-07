using FluentValidation;
using System.Linq;

namespace NEFBDAACommons.Shared.Models.Validation
{
    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        protected BaseValidator()
        {
        }

        protected BaseValidator(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        private string ErrorMessage { get; }

        public void ValidateThrow(T instance)
        {
            if (instance == null || Equals(instance, default(T)))
            {
                throw new ValidationException(ErrorMessage);
            }
            var result = Validate(instance);

            if (result.IsValid)
            {
                return;
            }

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                throw new ValidationException(ErrorMessage);
            }

            throw new ValidationException(string.Join(" ", result.Errors.Select(x => x.ErrorMessage)));
        }
    }
}
