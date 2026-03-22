using FluentValidation;

namespace SkillTrackerServer.Application.DevelopmentPlans.Archive
{
    internal sealed class ArchiveDevelopmentPlanCommandValidator
        : AbstractValidator<ArchiveDevelopmentPlanCommand>
    {
        public ArchiveDevelopmentPlanCommandValidator()
        {
            RuleFor(c => c.PlanId).NotEmpty();
        }
    }
}
