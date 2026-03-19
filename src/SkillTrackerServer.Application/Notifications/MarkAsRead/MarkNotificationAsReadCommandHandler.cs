using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.Notifications;
using SkillTrackerServer.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace SkillTrackerServer.Application.Notifications.MarkAsRead
{
    internal sealed class MarkNotificationAsReadCommandHandler(IApplicationDbContext context, IUserContext userContext)
        : ICommandHandler<MarkNotificationAsReadCommand>
    {
        public async Task<Result> Handle(MarkNotificationAsReadCommand command, CancellationToken cancellationToken)
        {
            Notification? notification = await context.Notifications.SingleOrDefaultAsync(n => n.Id == command.NotificationId && n.UserId == userContext.UserId, cancellationToken);

            if (notification == null)
            {
                return Result.Failure(NotificationErrors.NotFound(command.NotificationId));
            }

            notification.MarkAsRead();
            //notification.Raise(new NotificationMarkAsReadDomainEvent(notification.Id));
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
