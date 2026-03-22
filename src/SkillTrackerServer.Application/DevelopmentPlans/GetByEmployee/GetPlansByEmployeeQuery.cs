using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.DevelopmentPlans.GetByEmployee
{
    public sealed record GetPlansByEmployeeQuery(Guid OrganizationId)
        : IQuery<List<DevelopmentPlanResponse>>;
}
