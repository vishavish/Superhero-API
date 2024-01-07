using FluentValidation;
using Superhero.Api.Entities.Auth;

namespace Superhero.Api.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(lm => lm.Username)
                .NotEmpty().WithMessage("Username must not be empty.");

            RuleFor(lm => lm.Password)
                .NotEmpty().WithMessage("Password must not be empty.");
        }
    }
}
