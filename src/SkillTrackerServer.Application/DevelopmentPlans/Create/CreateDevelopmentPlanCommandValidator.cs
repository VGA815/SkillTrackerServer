using FluentValidation;

namespace SkillTrackerServer.Application.DevelopmentPlans.Create
{
    internal sealed class CreateDevelopmentPlanCommandValidator
        : AbstractValidator<CreateDevelopmentPlanCommand>
    {
        public CreateDevelopmentPlanCommandValidator()
        {
            RuleFor(c => c.EmployeeId).NotEmpty();
            RuleFor(c => c.OrganizationId).NotEmpty();
            RuleFor(c => c.Title).NotEmpty().MaximumLength(300);
            RuleFor(c => c.Description).MaximumLength(2000).When(c => c.Description is not null);
            RuleFor(c => c.EndDate)
                .GreaterThan(c => c.StartDate)
                .When(c => c.StartDate.HasValue && c.EndDate.HasValue)
                .WithMessage("EndDate must be later than StartDate");
        }
    }
}
