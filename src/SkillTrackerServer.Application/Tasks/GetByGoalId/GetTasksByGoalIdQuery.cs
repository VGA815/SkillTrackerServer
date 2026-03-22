using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Tasks.GetByGoalId
{
    public sealed record GetTasksByGoalIdQuery(Guid PlanId, Guid GoalId)
        : IQuery<List<DevelopmentTaskResponse>>;
}
