using FluentValidation;

namespace SkillTrackerServer.Application.Organizations.Create
{
    internal sealed class CreateOrganizationCommandValidator : AbstractValidator<CreateOrganizationCommand>
    {
        public CreateOrganizationCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(c => c.Description)
                .MaximumLength(1000)
                .When(c => c.Description is not null);
        }
    }
}
