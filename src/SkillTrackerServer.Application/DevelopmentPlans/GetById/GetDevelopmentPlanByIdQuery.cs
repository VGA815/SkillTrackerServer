using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.DevelopmentPlans.GetById
{
    public sealed record GetDevelopmentPlanByIdQuery(Guid PlanId)
        : IQuery<DevelopmentPlanResponse>, ICacheableQuery
    {
        public string CacheKey => $"v1:plans:{PlanId}";
        public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
    }
}
