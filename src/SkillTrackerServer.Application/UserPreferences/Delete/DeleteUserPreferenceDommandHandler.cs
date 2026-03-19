using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.UserPreferences;
using SkillTrackerServer.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace SkillTrackerServer.Application.UserPreferences.Delete
{
    internal sealed class DeleteUserPreferenceDommandHandler(IApplicationDbContext context, IUserContext userContext)
        : ICommandHandler<DeleteUserPreferenceCommand>
    {
        public async Task<Result> Handle(DeleteUserPreferenceCommand command, CancellationToken cancellationToken)
        {
            UserPreference? userPreference = await context.UserPreferences
                .SingleOrDefaultAsync(up => up.UserId == command.UserId && up.UserId == userContext.UserId, cancellationToken);

            if (userPreference is null)
            {
                return Result.Failure(UserPreferenceErrors.NotFound(command.UserId));
            }

            context.UserPreferences.Remove(userPreference);

            await  context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
