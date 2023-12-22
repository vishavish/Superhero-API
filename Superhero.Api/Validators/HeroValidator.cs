using FluentValidation;
using Superhero.Api.Entities;

namespace Superhero.Api.Validators
{
    public class HeroValidator : AbstractValidator<Hero>
    {
        public HeroValidator()
        {
            RuleFor(h => h.HeroName)
                .NotEmpty().WithMessage("Hero Name is required.")
                .MaximumLength(50).WithMessage("Length must not be more than 50 characters");

            RuleFor(h => h.Superpower)
                .NotEmpty().WithMessage("Superpower is required.");

            RuleFor(h => h.PowerLevel)
                //.NotEmpty().WithMessage("Power Level is required.")
                .GreaterThan(0).WithMessage("Invalid power level.");
        }
    }
}
