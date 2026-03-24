
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Application.Users.GetById;
using SkillTrackerServer.SharedKernel;
using SkillTrackerServer.WebApi.Extensions;
using SkillTrackerServer.WebApi.Infrastructure;

namespace SkillTrackerServer.WebApi.Endpoints.Users
{
    internal sealed class GetById : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("users/{userId:guid}", async (
                Guid userId,
                IQueryHandler<GetUserByIdQuery, UserResponse> handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetUserByIdQuery(userId);

                Result<UserResponse> result = await handler.Handle(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
                .RequireAuthorization()
                .WithTags(Tags.Users);
        }
    }
}
