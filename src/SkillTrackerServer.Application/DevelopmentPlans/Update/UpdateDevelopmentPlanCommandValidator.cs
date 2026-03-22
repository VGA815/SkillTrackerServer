using FluentValidation;

namespace SkillTrackerServer.Application.DevelopmentPlans.Update
{
    internal sealed class UpdateDevelopmentPlanCommandValidator
        : AbstractValidator<UpdateDevelopmentPlanCommand>
    {
        public UpdateDevelopmentPlanCommandValidator()
        {
            RuleFor(c => c.PlanId).NotEmpty();
            RuleFor(c => c.Title).NotEmpty().MaximumLength(300);
            RuleFor(c => c.Description).MaximumLength(2000).When(c => c.Description is not null);
            RuleFor(c => c.EndDate)
                .GreaterThan(c => c.StartDate)
                .When(c => c.StartDate.HasValue && c.EndDate.HasValue)
                .WithMessage("EndDate must be later than StartDate");
        }
    }
}
