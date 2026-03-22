using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.DevelopmentPlans;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.Goals.GetByPlanId
{
    internal sealed class GetGoalsByPlanIdQueryHandler(
        IApplicationDbContext context,
        IUserContext userContext)
        : IQueryHandler<GetGoalsByPlanIdQuery, List<GoalResponse>>
    {
        public async Task<Result<List<GoalResponse>>> Handle(
            GetGoalsByPlanIdQuery query,
            CancellationToken cancellationToken)
        {
            DevelopmentPlan? plan = await context.DevelopmentPlans
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == query.PlanId, cancellationToken);

            if (plan is null)
                return Result.Failure<List<GoalResponse>>(DevelopmentPlanErrors.NotFound(query.PlanId));

            bool hasAccess = plan.ManagerId == userContext.UserId
                          || plan.EmployeeId == userContext.UserId;

            if (!hasAccess)
                return Result.Failure<List<GoalResponse>>(DevelopmentPlanErrors.AccessDenied);

            List<GoalResponse> goals = await context.Goals
                .Where(g => g.PlanId == query.PlanId)
                .OrderBy(g => g.OrderIndex)
                .Select(g => new GoalResponse
                {
                    Id          = g.Id,
                    PlanId      = g.PlanId,
                    Title       = g.Title,
                    Description = g.Description,
                    SkillArea   = g.SkillArea,
                    OrderIndex  = g.OrderIndex,
                    CreatedAt   = g.CreatedAt,
                    UpdatedAt   = g.UpdatedAt,
                })
                .ToListAsync(cancellationToken);

            return goals;
        }
    }
}
