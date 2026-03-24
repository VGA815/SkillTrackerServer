
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Application.Users.Register;
using SkillTrackerServer.SharedKernel;
using SkillTrackerServer.WebApi.Extensions;
using SkillTrackerServer.WebApi.Infrastructure;
using System.Text.Json.Serialization;

namespace SkillTrackerServer.WebApi.Endpoints.Users
{
    internal sealed class Register : IEndpoint
    {
        public sealed record Request(
            [property: JsonPropertyName("email")] string Email,
            [property: JsonPropertyName("username")] string Username,
            [property: JsonPropertyName("password")] string Password,
            [property: JsonPropertyName("first_name")] string FirstName,
            [property: JsonPropertyName("last_name")] string? LastName);

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("users/register", async (
                Request request,
                ICommandHandler<RegisterUserCommand, Guid> handler,
                CancellationToken cancellationToken) =>
            {
                var command = new RegisterUserCommand(
                    request.Email,
                    request.Password,
                    request.Username,
                    request.FirstName,
                    request.LastName);

                Result<Guid> result = await handler.Handle(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Users);
        }
    }
}
