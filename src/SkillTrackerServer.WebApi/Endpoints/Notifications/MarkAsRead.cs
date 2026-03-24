using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Application.Notifications.MarkAsRead;
using SkillTrackerServer.SharedKernel;
using SkillTrackerServer.WebApi.Extensions;
using SkillTrackerServer.WebApi.Infrastructure;

namespace SkillTrackerServer.WebApi.Endpoints.Notifications
{
    internal sealed class MarkAsRead : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("/notifications/{notificationId:guid}", 
                async (Guid notificationId, ICommandHandler<MarkNotificationAsReadCommand> handler, CancellationToken cancellationToken) =>
                {
                    MarkNotificationAsReadCommand command = new(notificationId);
                    Result result = await handler.Handle(command, cancellationToken);
                    return result.Match(Results.NoContent, CustomResults.Problem);
                })
                    .RequireAuthorization()
                    .WithTags(Tags.Notifications);
        }
    }
}
