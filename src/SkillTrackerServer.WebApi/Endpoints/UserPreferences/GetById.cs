
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Application.UserPreferences.GetById;
using SkillTrackerServer.SharedKernel;
using SkillTrackerServer.WebApi.Extensions;
using SkillTrackerServer.WebApi.Infrastructure;

namespace SkillTrackerServer.WebApi.Endpoints.UserPreferences
{
    internal sealed class GetById : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("users/preferences/{preferenceId:guid}", async (
                Guid preferenceId, 
                IQueryHandler<GetUserPreferenceByIdQuery, UserPreferenceResponse> handler, 
                CancellationToken cancellationToken) =>
            {
                var query = new GetUserPreferenceByIdQuery(preferenceId);

                Result<UserPreferenceResponse> result = await handler.Handle(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
                .RequireAuthorization()
                .WithTags(Tags.UserPreferences);
        }
    }
}
