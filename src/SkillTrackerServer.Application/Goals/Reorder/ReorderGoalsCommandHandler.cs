using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.DevelopmentPlans;
using SkillTrackerServer.Domain.Goals;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.Goals.Reorder
{
    internal sealed class ReorderGoalsCommandHandler(
        IApplicationDbContext context,
        IUserContext userContext)
        : ICommandHandler<ReorderGoalsCommand>
    {
        public async Task<Result> Handle(ReorderGoalsCommand command, CancellationToken cancellationToken)
        {
            DevelopmentPlan? plan = await context.DevelopmentPlans
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == command.PlanId, cancellationToken);

            if (plan is null)
                return Result.Failure(DevelopmentPlanErrors.NotFound(command.PlanId));

            if (plan.ManagerId != userContext.UserId)
                return Result.Failure(DevelopmentPlanErrors.AccessDenied);

            if (plan.Status == DevelopmentPlanStatus.Archived)
                return Result.Failure(DevelopmentPlanErrors.CannotModifyArchived);

            List<Goal> goals = await context.Goals
                .Where(g => g.PlanId == command.PlanId)
                .ToListAsync(cancellationToken);

            if (goals.Count != command.OrderedGoalIds.Count)
                return Result.Failure(GoalErrors.ReorderCountMismatch);

            var goalDict = goals.ToDictionary(g => g.Id);

            for (int i = 0; i < command.OrderedGoalIds.Count; i++)
            {
                Guid id = command.OrderedGoalIds[i];

                if (!goalDict.TryGetValue(id, out Goal? goal))
                    return Result.Failure(GoalErrors.NotFound(id));

                goal.OrderIndex = i;
            }

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
