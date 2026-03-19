using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.EmailVerificationTokens.ResendEmailVerification
{
    public sealed record ResendEmailVerificationCommand(string Email) : ICommand;
}
