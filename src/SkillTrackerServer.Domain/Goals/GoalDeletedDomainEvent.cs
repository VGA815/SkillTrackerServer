using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Goals
{
    public sealed record GoalDeletedDomainEvent(Guid GoalId, Guid PlanId) : IDomainEvent;
}
