using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.DevelopmentPlans;
using SkillTrackerServer.Domain.Tasks;
using SkillTrackerServer.SharedKernel;
using TaskStatus = SkillTrackerServer.Domain.Tasks.TaskStatus;

namespace SkillTrackerServer.Application.Tasks.Complete
{
    internal sealed class CompleteTaskCommandHandler(
        IApplicationDbContext context,
        IUserContext userContext,
        IDateTimeProvider dateTimeProvider)
        : ICommandHandler<CompleteTaskCommand>
    {
        public async Task<Result> Handle(CompleteTaskCommand command, CancellationToken cancellationToken)
        {
            DevelopmentPlan? plan = await context.DevelopmentPlans
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == command.PlanId, cancellationToken);

            if (plan is null)
                return Result.Failure(DevelopmentPlanErrors.NotFound(command.PlanId));

            // Only the employee can complete a task
            if (plan.EmployeeId != userContext.UserId)
                return Result.Failure(DevelopmentTaskErrors.AccessDenied);

            DevelopmentTask? task = await context.DevelopmentTasks
                .SingleOrDefaultAsync(t => t.Id == command.TaskId, cancellationToken);

            if (task is null)
                return Result.Failure(DevelopmentTaskErrors.NotFound(command.TaskId));

            if (task.GoalId != command.GoalId)
                return Result.Failure(DevelopmentTaskErrors.DoesNotBelongToGoal);

            if (task.Status == TaskStatus.Completed)
                return Result.Failure(DevelopmentTaskErrors.AlreadyCompleted(command.TaskId));

            if (task.Status != TaskStatus.InProgress && task.Status != TaskStatus.Overdue)
                return Result.Failure(DevelopmentTaskErrors.CannotComplete(command.TaskId, task.Status));

            DateTime completedAt = dateTimeProvider.UtcNow;
            task.Status      = TaskStatus.Completed;
            task.CompletedAt = completedAt;
            task.UpdatedAt   = completedAt;

            task.Raise(new TaskCompletedDomainEvent(task.Id, task.GoalId, task.PlanId, completedAt));

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
