using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Application.Users.GetByEmail;
using SkillTrackerServer.SharedKernel;
using SkillTrackerServer.WebApi.Extensions;
using SkillTrackerServer.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace SkillTrackerServer.WebApi.Endpoints.Users
{
    internal sealed class GetByEmail : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("users", async (
                [FromQuery] string email, 
                IQueryHandler<GetUserByEmailQuery, UserResponse> handler, 
                CancellationToken cancellationToken) =>
            {
                var query = new GetUserByEmailQuery(email);

                Result<UserResponse> result = await handler.Handle(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
                .RequireAuthorization()
                .WithTags(Tags.Users);
        }
    }
}