using FluentValidation;
using Superhero.Api.Entities;

namespace Superhero.Api.Validators
{
    public class OrganizationValidator : AbstractValidator<Organization>
    {
        public OrganizationValidator()
        {
            RuleFor(o => o.Name)
                .NotEmpty().WithMessage("Name should not be empty.")
                .MaximumLength(50).WithMessage("Length must not be more than 50 characters.");
        }
    }
}
