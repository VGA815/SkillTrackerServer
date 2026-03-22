using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Goals.GetByPlanId
{
    public sealed record GetGoalsByPlanIdQuery(Guid PlanId) : IQuery<List<GoalResponse>>;
}
