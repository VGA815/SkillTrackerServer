using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Goals
{
    public sealed record GoalCreatedDomainEvent(Guid GoalId, Guid PlanId) : IDomainEvent;
}
