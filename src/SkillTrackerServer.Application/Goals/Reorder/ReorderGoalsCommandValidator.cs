using FluentValidation;

namespace SkillTrackerServer.Application.Goals.Reorder
{
    internal sealed class ReorderGoalsCommandValidator : AbstractValidator<ReorderGoalsCommand>
    {
        public ReorderGoalsCommandValidator()
        {
            RuleFor(c => c.PlanId).NotEmpty();
            RuleFor(c => c.OrderedGoalIds).NotEmpty();
        }
    }
}
