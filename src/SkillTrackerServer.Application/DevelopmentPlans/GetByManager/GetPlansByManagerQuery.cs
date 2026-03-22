using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.DevelopmentPlans.GetByManager
{
    public sealed record GetPlansByManagerQuery(Guid OrganizationId)
        : IQuery<List<DevelopmentPlanResponse>>;
}
