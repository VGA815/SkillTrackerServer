using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Notifications;
using SkillTrackerServer.Domain.DevelopmentPlans;
using SkillTrackerServer.Domain.Notifications;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.DevelopmentPlans
{
    internal sealed class DevelopmentPlanCreatedDomainEventHandler(
        IApplicationDbContext context,
        INotificationSender notificationSender,
        IDateTimeProvider dateTimeProvider)
        : IDomainEventHandler<DevelopmentPlanCreatedDomainEvent>
    {
        public async Task Handle(
            DevelopmentPlanCreatedDomainEvent domainEvent,
            CancellationToken cancellationToken)
        {
            Notification notification = Notification.Create(
                userId:      domainEvent.EmployeeId,
                type:        "PlanCreated",
                title:       "New development plan assigned",
                body:        "Your manager has created a new development plan for you.",
                createdAt:   dateTimeProvider.UtcNow,
                referenceId: domainEvent.PlanId);

            context.Notifications.Add(notification);
            await context.SaveChangesAsync(cancellationToken);

            await notificationSender.SendAsync(
                notification.Id,
                notification.UserId,
                notification.Type,
                notification.Title,
                notification.Body,
                notification.CreatedAt,
                notification.ReferenceId,
                cancellationToken);
        }
    }
}
