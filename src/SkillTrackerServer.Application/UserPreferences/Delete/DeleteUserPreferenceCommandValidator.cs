using FluentValidation;

namespace SkillTrackerServer.Application.UserPreferences.Delete
{
    internal sealed class DeleteUserPreferenceCommandValidator : AbstractValidator<DeleteUserPreferenceCommand>
    {
        public DeleteUserPreferenceCommandValidator()
        {
            RuleFor(up => up.UserId).NotEmpty();
        }
    }
}
