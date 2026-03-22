using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Goals.Update
{
    public sealed record UpdateGoalCommand(
        Guid PlanId,
        Guid GoalId,
        string Title,
        string? Description,
        string? SkillArea) : ICommand;
}
