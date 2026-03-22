using FluentValidation;

namespace SkillTrackerServer.Application.Tasks.Start
{
    internal sealed class StartTaskCommandValidator : AbstractValidator<StartTaskCommand>
    {
        public StartTaskCommandValidator()
        {
            RuleFor(c => c.PlanId).NotEmpty();
            RuleFor(c => c.GoalId).NotEmpty();
            RuleFor(c => c.TaskId).NotEmpty();
        }
    }
}
