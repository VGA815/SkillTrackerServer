using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.UserPreferences;
using SkillTrackerServer.Domain.Users;
using SkillTrackerServer.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace SkillTrackerServer.Application.Users.Register
{
    internal sealed class RegisterUserCommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher, IDateTimeProvider dateTimeProvider)
        : ICommandHandler<RegisterUserCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            if (await context.Users.AnyAsync(u => u.Email == command.Email, cancellationToken))
            {
                return Result.Failure<Guid>(UserErrors.EmailNotUnique);
            }

            User user = User.Create(command.Username, command.FirstName, command.LastName, command.Email, passwordHasher.Hash(command.Password), command.UserRole, dateTimeProvider.UtcNow);
            UserPreference userPreference = UserPreference.CreateDefault(user.Id);


            context.Users.Add(user);
            context.UserPreferences.Add(userPreference);

            await context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
