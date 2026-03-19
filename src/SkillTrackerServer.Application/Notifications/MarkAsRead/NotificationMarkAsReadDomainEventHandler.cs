using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Domain.Notifications;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.Notifications.MarkAsRead
{
    //internal sealed class NotificationMarkAsReadDomainEventHandler(ICacheService cacheService) : IDomainEventHandler<NotificationMarkAsReadDomainEvent>
    //{
    //    public async Task Handle(NotificationMarkAsReadDomainEvent domainEvent, CancellationToken cancellationToken)
    //    {
    //        var key = $"v1:notifications:{domainEvent.NotificationId}";
    //        await cacheService.RemoveAsync(key);
    //    }
    //}
}