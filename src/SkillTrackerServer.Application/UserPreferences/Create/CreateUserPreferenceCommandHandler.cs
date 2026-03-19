using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.UserPreferences;
using SkillTrackerServer.Domain.Users;
using SkillTrackerServer.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace SkillTrackerServer.Application.UserPreferences.Create
{
    internal sealed class CreateUserPreferenceCommandHandler(IApplicationDbContext context, IUserContext userContext)
        : ICommandHandler<CreateUserPreferenceCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateUserPreferenceCommand command, CancellationToken cancellationToken)
        {
            if (command.UserId != userContext.UserId)
            {
                return Result.Failure<Guid>(UserErrors.Unauthorized());
            }

            User? user = await context.Users.AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

            if (user == null)
            {
                return Result.Failure<Guid>(UserErrors.NotFound(command.UserId));
            }

            UserPreference userPreference = UserPreference.Create(userContext.UserId, command.Theme, command.ReceiveNotifications);

            context.UserPreferences.Add(userPreference);

            await context.SaveChangesAsync(cancellationToken);

            return userPreference.UserId;
        }
    }
}
