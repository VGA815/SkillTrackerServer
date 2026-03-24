using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Application.EmailVerificationTokens.ResendEmailVerification;
using SkillTrackerServer.SharedKernel;
using SkillTrackerServer.WebApi.Extensions;
using SkillTrackerServer.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace SkillTrackerServer.WebApi.Endpoints.EmailVerificationTokens
{
    internal sealed class Resend : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("email-verification/resend", async (
                [FromQuery] string email, 
                ICommandHandler<ResendEmailVerificationCommand> innerHandler,
                CancellationToken cancellationToken) => 
            { 
                var command = new ResendEmailVerificationCommand(email);

                Result result = await innerHandler.Handle(command, cancellationToken);

                return result.Match(Results.NoContent, CustomResults.Problem);
            })
                .WithTags(Tags.EmailVerification);
        }
    }
}
