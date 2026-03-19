using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.UserPreferences;
using SkillTrackerServer.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace SkillTrackerServer.Application.UserPreferences.Update
{
    internal sealed class UpdateUserPreferenceCommandHandler(IApplicationDbContext context, IUserContext userContext)
        : ICommandHandler<UpdateUserPreferenceCommand>
    {
        public async Task<Result> Handle(UpdateUserPreferenceCommand command, CancellationToken cancellationToken)
        {
            UserPreference? userPreference = await context.UserPreferences
                .SingleOrDefaultAsync(up => up.UserId == command.UserId && up.UserId == userContext.UserId, cancellationToken);

            if (userPreference == null)
            {
                return Result.Failure(UserPreferenceErrors.NotFound(command.UserId));
            }

            userPreference.ReceiveNotifications = command.ReceiveNotifications;
            userPreference.Theme = command.Theme;


            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
