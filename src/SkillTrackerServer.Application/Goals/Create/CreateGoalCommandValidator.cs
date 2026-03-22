using FluentValidation;

namespace SkillTrackerServer.Application.Goals.Create
{
    internal sealed class CreateGoalCommandValidator : AbstractValidator<CreateGoalCommand>
    {
        public CreateGoalCommandValidator()
        {
            RuleFor(c => c.PlanId).NotEmpty();
            RuleFor(c => c.Title).NotEmpty().MaximumLength(300);
            RuleFor(c => c.Description).MaximumLength(2000).When(c => c.Description is not null);
            RuleFor(c => c.SkillArea).MaximumLength(200).When(c => c.SkillArea is not null);
        }
    }
}
