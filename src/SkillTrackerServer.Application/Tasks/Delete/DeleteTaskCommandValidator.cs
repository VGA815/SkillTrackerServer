using FluentValidation;

namespace SkillTrackerServer.Application.Tasks.Delete
{
    internal sealed class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
    {
        public DeleteTaskCommandValidator()
        {
            RuleFor(c => c.PlanId).NotEmpty();
            RuleFor(c => c.GoalId).NotEmpty();
            RuleFor(c => c.TaskId).NotEmpty();
        }
    }
}
