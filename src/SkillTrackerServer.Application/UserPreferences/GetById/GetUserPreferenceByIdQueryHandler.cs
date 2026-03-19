using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.UserPreferences;
using SkillTrackerServer.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace SkillTrackerServer.Application.UserPreferences.GetById
{
    internal sealed class GetUserPreferenceByIdQueryHandler(IApplicationDbContext context, IUserContext userContext)
        : IQueryHandler<GetUserPreferenceByIdQuery, UserPreferenceResponse>
    {
        public async Task<Result<UserPreferenceResponse>> Handle(GetUserPreferenceByIdQuery query, CancellationToken cancellationToken)
        {

            UserPreferenceResponse? userPreference = await context.UserPreferences
                .Where(up => up.UserId == query.UserId && up.UserId == userContext.UserId)
                .Select(up => new UserPreferenceResponse
                {
                    UserId = up.UserId,
                    ReceiveNotifications = up.ReceiveNotifications,
                    Theme = up.Theme,
                })
                .SingleOrDefaultAsync(cancellationToken);

            if (userPreference is null)
            {
                return Result.Failure<UserPreferenceResponse>(UserPreferenceErrors.NotFound(query.UserId));
            }

            return userPreference;
        }
    }
}
