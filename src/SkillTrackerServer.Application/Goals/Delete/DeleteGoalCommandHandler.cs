using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.DevelopmentPlans;
using SkillTrackerServer.Domain.Goals;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.Goals.Delete
{
    internal sealed class DeleteGoalCommandHandler(
        IApplicationDbContext context,
        IUserContext userContext)
        : ICommandHandler<DeleteGoalCommand>
    {
        public async Task<Result> Handle(DeleteGoalCommand command, CancellationToken cancellationToken)
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

            Goal? goal = await context.Goals
                .SingleOrDefaultAsync(g => g.Id == command.GoalId, cancellationToken);

            if (goal is null)
                return Result.Failure(GoalErrors.NotFound(command.GoalId));

            if (goal.PlanId != command.PlanId)
                return Result.Failure(GoalErrors.DoesNotBelongToPlan);

            // Remove all tasks that belong to this goal
            var tasks = await context.DevelopmentTasks
                .Where(t => t.GoalId == command.GoalId)
                .ToListAsync(cancellationToken);

            context.DevelopmentTasks.RemoveRange(tasks);

            goal.Raise(new GoalDeletedDomainEvent(goal.Id, goal.PlanId));
            context.Goals.Remove(goal);

            // Re-index remaining goals
            var remaining = await context.Goals
                .Where(g => g.PlanId == command.PlanId && g.Id != command.GoalId)
                .OrderBy(g => g.OrderIndex)
                .ToListAsync(cancellationToken);

            for (int i = 0; i < remaining.Count; i++)
                remaining[i].OrderIndex = i;

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
