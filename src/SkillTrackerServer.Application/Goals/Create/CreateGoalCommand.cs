using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Goals.Create
{
    public sealed record CreateGoalCommand(
        Guid PlanId,
        string Title,
        string? Description,
        string? SkillArea) : ICommand<Guid>;
}
