using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Application.Notifications.GetByUserId;
using SkillTrackerServer.SharedKernel;
using SkillTrackerServer.WebApi.Extensions;
using SkillTrackerServer.WebApi.Infrastructure;

namespace SkillTrackerServer.WebApi.Endpoints.Notifications
{
    internal sealed class GetByUserId : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("notifications/user", async (
                int page,
                int pageSize,
                IQueryHandler<GetNotificationsByUserIdQuery, List<NotificationResponse>> handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetNotificationsByUserIdQuery(page, pageSize);
                Result<List<NotificationResponse>> result = await handler.Handle(query, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
                .RequireAuthorization()
                .WithTags(Tags.Notifications);
        }
    }
}
