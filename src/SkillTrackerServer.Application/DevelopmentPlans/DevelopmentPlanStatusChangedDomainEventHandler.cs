using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Notifications;
using SkillTrackerServer.Domain.DevelopmentPlans;
using SkillTrackerServer.Domain.Notifications;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.DevelopmentPlans.DomainEventHandlers
{
    internal sealed class DevelopmentPlanStatusChangedDomainEventHandler(
        IApplicationDbContext context,
        INotificationSender notificationSender,
        IDateTimeProvider dateTimeProvider)
        : IDomainEventHandler<DevelopmentPlanStatusChangedDomainEvent>
    {
        public async Task Handle(
            DevelopmentPlanStatusChangedDomainEvent domainEvent,
            CancellationToken cancellationToken)
        {
            DevelopmentPlan? plan = await context.DevelopmentPlans
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == domainEvent.PlanId, cancellationToken);

            if (plan is null)
                return;

            string title = domainEvent.NewStatus switch
            {
                DevelopmentPlanStatus.Active   => "Development plan activated",
                DevelopmentPlanStatus.Archived => "Development plan archived",
                _                              => "Development plan updated",
            };

            string body = domainEvent.NewStatus switch
            {
                DevelopmentPlanStatus.Active   => "Your manager has activated your development plan. Time to get started!",
                DevelopmentPlanStatus.Archived => "Your development plan has been archived.",
                _                              => "Your development plan status has changed.",
            };

            Notification notification = Notification.Create(
                userId:      plan.EmployeeId,
                type:        $"PlanStatus{domainEvent.NewStatus}",
                title:       title,
                body:        body,
                createdAt:   dateTimeProvider.UtcNow,
                referenceId: plan.Id);

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
