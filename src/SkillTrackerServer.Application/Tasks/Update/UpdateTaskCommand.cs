using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Tasks.Update
{
    public sealed record UpdateTaskCommand(
        Guid PlanId,
        Guid GoalId,
        Guid TaskId,
        string Title,
        string? Description,
        DateTime? DueDate) : ICommand;
}
