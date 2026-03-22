using FluentValidation;

namespace SkillTrackerServer.Application.Goals.Delete
{
    internal sealed class DeleteGoalCommandValidator : AbstractValidator<DeleteGoalCommand>
    {
        public DeleteGoalCommandValidator()
        {
            RuleFor(c => c.PlanId).NotEmpty();
            RuleFor(c => c.GoalId).NotEmpty();
        }
    }
}
