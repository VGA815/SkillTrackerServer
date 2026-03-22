using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Tasks
{
    public sealed record TaskCompletedDomainEvent(Guid TaskId, Guid GoalId, Guid PlanId, DateTime CompletedAt) : IDomainEvent;
}
