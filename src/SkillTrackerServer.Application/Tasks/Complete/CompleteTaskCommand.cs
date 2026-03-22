using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Tasks.Complete
{
    public sealed record CompleteTaskCommand(Guid PlanId, Guid GoalId, Guid TaskId) : ICommand;
}
