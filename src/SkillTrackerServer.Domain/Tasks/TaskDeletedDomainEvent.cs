using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Tasks
{
    public sealed record TaskDeletedDomainEvent(Guid TaskId, Guid GoalId, Guid PlanId) : IDomainEvent;
}
