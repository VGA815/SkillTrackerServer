using FluentValidation;

namespace SkillTrackerServer.Application.Tasks.Update
{
    internal sealed class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            RuleFor(c => c.PlanId).NotEmpty();
            RuleFor(c => c.GoalId).NotEmpty();
            RuleFor(c => c.TaskId).NotEmpty();
            RuleFor(c => c.Title).NotEmpty().MaximumLength(300);
            RuleFor(c => c.Description).MaximumLength(2000).When(c => c.Description is not null);
        }
    }
}
