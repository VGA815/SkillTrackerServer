using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Notifications.GetById
{
    public sealed record GetNotificationByIdQuery(Guid NotificationId) : IQuery<NotificationResponse>, ICacheableQuery
    {
        public string CacheKey => $"v1:notifications:{NotificationId}";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
    }
}
