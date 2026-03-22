using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Tasks
{
    public sealed record TaskOverdueDomainEvent(Guid TaskId, Guid GoalId, Guid PlanId) : IDomainEvent;
}
