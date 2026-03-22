using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Tasks.Create
{
    public sealed record CreateTaskCommand(
        Guid PlanId,
        Guid GoalId,
        string Title,
        string? Description,
        DateTime? DueDate) : ICommand<Guid>;
}
