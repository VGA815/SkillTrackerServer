using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Goals.Reorder
{
    /// OrderedGoalIds — полный упорядоченный список Id целей плана.
    public sealed record ReorderGoalsCommand(
        Guid PlanId,
        List<Guid> OrderedGoalIds) : ICommand;
}
