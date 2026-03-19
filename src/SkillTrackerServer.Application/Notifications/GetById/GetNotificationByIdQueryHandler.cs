using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.Notifications;
using SkillTrackerServer.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace SkillTrackerServer.Application.Notifications.GetById
{
    internal sealed class GetNotificationByIdQueryHandler(IApplicationDbContext context, IUserContext userContext)
        : IQueryHandler<GetNotificationByIdQuery, NotificationResponse>
    {
        async Task<Result<NotificationResponse>> IQueryHandler<GetNotificationByIdQuery, NotificationResponse>.Handle(GetNotificationByIdQuery query, CancellationToken cancellationToken)
        {
            NotificationResponse? notification = await context.Notifications
                .Where(n => n.Id == query.NotificationId && n.UserId == userContext.UserId)
                .Select(n => new NotificationResponse
                {
                    Id = n.Id,
                    UserId = n.UserId,
                    Type = n.Type,
                    Title = n.Title,
                    Body = n.Body,
                    ReferenceId = n.ReferenceId,
                    IsRead = n.IsRead,
                    CreatedAt = n.CreatedAt
                })
                .SingleOrDefaultAsync(cancellationToken);
            if (notification is null)
            {
                return Result.Failure<NotificationResponse>(NotificationErrors.NotFound(query.NotificationId));
            }
            return notification;
        }
    }
}
