using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Users.GetById
{
    public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>, ICacheableQuery
    {
        public string CacheKey => $"v1:user:{UserId}";

        public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
    }
}
