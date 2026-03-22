using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.DevelopmentPlans;
using SkillTrackerServer.Domain.Goals;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.Tasks.GetByGoalId
{
    internal sealed class GetTasksByGoalIdQueryHandler(
        IApplicationDbContext context,
        IUserContext userContext)
        : IQueryHandler<GetTasksByGoalIdQuery, List<DevelopmentTaskResponse>>
    {
        public async Task<Result<List<DevelopmentTaskResponse>>> Handle(
            GetTasksByGoalIdQuery query,
            CancellationToken cancellationToken)
        {
            DevelopmentPlan? plan = await context.DevelopmentPlans
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == query.PlanId, cancellationToken);

            if (plan is null)
                return Result.Failure<List<DevelopmentTaskResponse>>(
                    DevelopmentPlanErrors.NotFound(query.PlanId));

            bool hasAccess = plan.ManagerId == userContext.UserId
                          || plan.EmployeeId == userContext.UserId;

            if (!hasAccess)
                return Result.Failure<List<DevelopmentTaskResponse>>(DevelopmentPlanErrors.AccessDenied);

            bool goalExists = await context.Goals
                .AnyAsync(g => g.Id == query.GoalId && g.PlanId == query.PlanId, cancellationToken);

            if (!goalExists)
                return Result.Failure<List<DevelopmentTaskResponse>>(GoalErrors.NotFound(query.GoalId));

            List<DevelopmentTaskResponse> tasks = await context.DevelopmentTasks
                .Where(t => t.GoalId == query.GoalId && t.PlanId == query.PlanId)
                .OrderBy(t => t.OrderIndex)
                .Select(t => new DevelopmentTaskResponse
                {
                    Id          = t.Id,
                    GoalId      = t.GoalId,
                    PlanId      = t.PlanId,
                    Title       = t.Title,
                    Description = t.Description,
                    Status      = t.Status,
                    DueDate     = t.DueDate,
                    CompletedAt = t.CompletedAt,
                    OrderIndex  = t.OrderIndex,
                    CreatedAt   = t.CreatedAt,
                    UpdatedAt   = t.UpdatedAt,
                })
                .ToListAsync(cancellationToken);

            return tasks;
        }
    }
}
