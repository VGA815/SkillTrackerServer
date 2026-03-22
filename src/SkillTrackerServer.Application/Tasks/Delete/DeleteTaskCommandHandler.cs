using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.DevelopmentPlans;
using SkillTrackerServer.Domain.Tasks;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.Tasks.Delete
{
    internal sealed class DeleteTaskCommandHandler(
        IApplicationDbContext context,
        IUserContext userContext)
        : ICommandHandler<DeleteTaskCommand>
    {
        public async Task<Result> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
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

            DevelopmentTask? task = await context.DevelopmentTasks
                .SingleOrDefaultAsync(t => t.Id == command.TaskId, cancellationToken);

            if (task is null)
                return Result.Failure(DevelopmentTaskErrors.NotFound(command.TaskId));

            if (task.GoalId != command.GoalId)
                return Result.Failure(DevelopmentTaskErrors.DoesNotBelongToGoal);

            task.Raise(new TaskDeletedDomainEvent(task.Id, task.GoalId, task.PlanId));
            context.DevelopmentTasks.Remove(task);

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
