using FluentValidation;

namespace SkillTrackerServer.Application.Notifications.MarkAsRead
{
    internal sealed class MarkNotificationAsReadCommandValidator : AbstractValidator<MarkNotificationAsReadCommand>
    {
        public MarkNotificationAsReadCommandValidator()
        {
            RuleFor(x => x.NotificationId).NotEmpty();
        }
    }
}
