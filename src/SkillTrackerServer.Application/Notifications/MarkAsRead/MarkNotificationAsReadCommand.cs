using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Notifications.MarkAsRead
{
    public sealed record MarkNotificationAsReadCommand(Guid NotificationId) : ICommand;
}
