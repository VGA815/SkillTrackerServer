using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Tasks
{
    public sealed record TaskStatusChangedDomainEvent(Guid TaskId, Guid GoalId, Guid PlanId, TaskStatus NewStatus) : IDomainEvent;
}
