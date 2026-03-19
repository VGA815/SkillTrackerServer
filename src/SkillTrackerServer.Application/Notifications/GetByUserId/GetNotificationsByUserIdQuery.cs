using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Notifications.GetByUserId
{
    public sealed record GetNotificationsByUserIdQuery(int Page, int PageSize) : IQuery<List<NotificationResponse>>;
}
