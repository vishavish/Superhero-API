using FluentValidation;
using Superhero.Api.Entities.Auth;

namespace Superhero.Api.Validators
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(rm => rm.Username)
                .NotEmpty().WithMessage("Username must not be empty");

            RuleFor(rm => rm.Password)
                .NotEmpty().WithMessage("Password must not be empty")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .Matches("^(?=.*[a-z])").WithMessage("Password must contain atleast one lowercase character.")
                .Matches("^(?=.*[A-Z])").WithMessage("Password must contain atleast one uppercase character.")
                .Matches("^(?=.*\\d)").WithMessage("Password must contain atleast one numeric character.")
                .Matches("^(?=.*[\\W_])").WithMessage("Password must contain atleast one alphanumeric character.");

            RuleFor(rm => rm.Email)
                .NotEmpty().WithMessage("Email must not be empty")
                .EmailAddress().WithMessage("Email is not a valid email address");
        }
    }
}
