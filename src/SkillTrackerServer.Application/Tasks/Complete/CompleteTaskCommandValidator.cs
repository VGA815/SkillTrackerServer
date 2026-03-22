using FluentValidation;

namespace SkillTrackerServer.Application.Tasks.Complete
{
    internal sealed class CompleteTaskCommandValidator : AbstractValidator<CompleteTaskCommand>
    {
        public CompleteTaskCommandValidator()
        {
            RuleFor(c => c.PlanId).NotEmpty();
            RuleFor(c => c.GoalId).NotEmpty();
            RuleFor(c => c.TaskId).NotEmpty();
        }
    }
}
