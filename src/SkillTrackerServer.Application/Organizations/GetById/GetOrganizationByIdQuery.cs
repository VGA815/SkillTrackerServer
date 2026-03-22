using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Organizations.GetById
{
    public sealed record GetOrganizationByIdQuery(Guid OrganizationId) : IQuery<OrganizationResponse>, ICacheableQuery
    {
        public string CacheKey => $"v1:organizations:{OrganizationId}";
        public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
    }
}
