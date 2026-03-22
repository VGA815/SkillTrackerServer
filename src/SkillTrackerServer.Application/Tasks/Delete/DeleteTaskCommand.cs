using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Tasks.Delete
{
    public sealed record DeleteTaskCommand(Guid PlanId, Guid GoalId, Guid TaskId) : ICommand;
}
