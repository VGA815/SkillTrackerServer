using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.UserPreferences.GetById
{
    public sealed record GetUserPreferenceByIdQuery(Guid UserId) : IQuery<UserPreferenceResponse>, ICacheableQuery
    {
        public string CacheKey => $"v1:user-preferences:{UserId}";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
    }
}
