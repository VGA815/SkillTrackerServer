using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Goals.Delete
{
    public sealed record DeleteGoalCommand(Guid PlanId, Guid GoalId) : ICommand;
}
