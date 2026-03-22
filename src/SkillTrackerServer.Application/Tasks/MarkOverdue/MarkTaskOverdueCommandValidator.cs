using FluentValidation;

namespace SkillTrackerServer.Application.Tasks.MarkOverdue
{
    internal sealed class MarkTaskOverdueCommandValidator : AbstractValidator<MarkTaskOverdueCommand>
    {
        public MarkTaskOverdueCommandValidator()
        {
            RuleFor(c => c.TaskId).NotEmpty();
        }
    }
}
