using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.EmailVerificationTokens;
using SkillTrackerServer.Domain.Users;
using SkillTrackerServer.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace SkillTrackerServer.Application.EmailVerificationTokens.ResendEmailVerification
{
    internal sealed class ResendEmailVerificationCommandHandler(IApplicationDbContext context, IEmailSender emailSender, IDateTimeProvider dateTimeProvider)
        : ICommandHandler<ResendEmailVerificationCommand>
    {
        public async Task<Result> Handle(ResendEmailVerificationCommand command, CancellationToken cancellationToken)
        {
            User? user = await context.Users.SingleOrDefaultAsync(x => x.Email == command.Email, cancellationToken);
            if (user == null) 
            {
                return Result.Failure(UserErrors.NotFoundByEmail);
            }
            EmailVerificationToken token = new()
            {
                CreatedAt = dateTimeProvider.UtcNow,
                ExpiresAt = dateTimeProvider.UtcNow + TimeSpan.FromMinutes(20),
                TokenId = Guid.NewGuid(),
                UserId = user.Id,
            };
            context.EmailVerificationTokens.Add(token);

            await context.SaveChangesAsync(cancellationToken);
            await emailSender.SendVerification(user.Email, token.TokenId.ToString());
            return Result.Success();
        }
    }
}
