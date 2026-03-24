
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Application.UserPreferences.Update;
using SkillTrackerServer.Domain.UserPreferences;
using SkillTrackerServer.SharedKernel;
using SkillTrackerServer.WebApi.Extensions;
using SkillTrackerServer.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace SkillTrackerServer.WebApi.Endpoints.UserPreferences
{
    internal sealed class Update : IEndpoint
    {
        public sealed record Request(
            [property: JsonPropertyName("user_id")] Guid UserId,
            [property: JsonPropertyName("theme")] UserPreferenceTheme Theme,
            [property: JsonPropertyName("receive_notifications")] bool ReceiveNotifications);
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("users/preferences", async (
                [FromBody] Request request, 
                ICommandHandler<UpdateUserPreferenceCommand> handler, 
                CancellationToken cancellationToken) =>
            {
                var command = new UpdateUserPreferenceCommand(request.UserId, request.Theme, request.ReceiveNotifications);

                Result result = await handler.Handle(command, cancellationToken);

                return result.Match(Results.NoContent, CustomResults.Problem);
            })
                .RequireAuthorization()
                .WithTags(Tags.UserPreferences);
        }
    }
}
