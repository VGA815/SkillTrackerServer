using FluentValidation;

namespace SkillTrackerServer.Application.DevelopmentPlans.Activate
{
    internal sealed class ActivateDevelopmentPlanCommandValidator
        : AbstractValidator<ActivateDevelopmentPlanCommand>
    {
        public ActivateDevelopmentPlanCommandValidator()
        {
            RuleFor(c => c.PlanId).NotEmpty();
        }
    }
}
