using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Tasks.Start
{
    public sealed record StartTaskCommand(Guid PlanId, Guid GoalId, Guid TaskId) : ICommand;
}
