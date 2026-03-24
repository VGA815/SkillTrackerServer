using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Application.Notifications.GetById;
using SkillTrackerServer.SharedKernel;
using SkillTrackerServer.WebApi.Extensions;
using SkillTrackerServer.WebApi.Infrastructure;

namespace SkillTrackerServer.WebApi.Endpoints.Notifications
{
    internal sealed class GetById : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/notifications/{notificationId:guid}", async (
                Guid notificationId, 
                IQueryHandler<GetNotificationByIdQuery, NotificationResponse> handler,
                CancellationToken cancellationToken) => 
            { 
                var query = new GetNotificationByIdQuery(notificationId);
                Result<NotificationResponse> result = await handler.Handle(query, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
                .RequireAuthorization()
                .WithTags(Tags.Notifications);
        }
    }
}
