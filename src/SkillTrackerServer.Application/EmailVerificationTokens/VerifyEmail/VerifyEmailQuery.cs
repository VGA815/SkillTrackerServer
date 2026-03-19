using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.EmailVerificationTokens.VerifyEmail
{
    public sealed record VerifyEmailQuery(Guid TokenId) : IQuery<EmailVerificationResponse>;
}
