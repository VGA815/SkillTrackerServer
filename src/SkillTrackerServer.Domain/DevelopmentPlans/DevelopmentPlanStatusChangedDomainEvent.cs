using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.DevelopmentPlans
{
    public sealed record DevelopmentPlanStatusChangedDomainEvent(Guid PlanId, DevelopmentPlanStatus NewStatus) : IDomainEvent;
}
