using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Application.EmailVerificationTokens.VerifyEmail;
using SkillTrackerServer.SharedKernel;
using SkillTrackerServer.WebApi.Extensions;
using SkillTrackerServer.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace SkillTrackerServer.WebApi.Endpoints.EmailVerificationTokens
{
    internal sealed class Verify : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("verify", async (
                [FromQuery] Guid token, 
                IQueryHandler<VerifyEmailQuery, EmailVerificationResponse> innerHandler, 
                CancellationToken cancellationToken) => 
            { 
                var query = new VerifyEmailQuery(token);
                Result<EmailVerificationResponse> result = await innerHandler.Handle(query, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
                .WithTags(Tags.EmailVerification)
                .WithName("VerifyEmail");
        }
    }
}
