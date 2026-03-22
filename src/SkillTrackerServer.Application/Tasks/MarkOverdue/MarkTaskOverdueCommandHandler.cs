using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.Tasks;
using SkillTrackerServer.SharedKernel;
using TaskStatus = SkillTrackerServer.Domain.Tasks.TaskStatus;

namespace SkillTrackerServer.Application.Tasks.MarkOverdue
{
    internal sealed class MarkTaskOverdueCommandHandler(
        IApplicationDbContext context,
        IDateTimeProvider dateTimeProvider)
        : ICommandHandler<MarkTaskOverdueCommand>
    {
        public async Task<Result> Handle(MarkTaskOverdueCommand command, CancellationToken cancellationToken)
        {
            DevelopmentTask? task = await context.DevelopmentTasks
                .SingleOrDefaultAsync(t => t.Id == command.TaskId, cancellationToken);

            if (task is null)
                return Result.Failure(DevelopmentTaskErrors.NotFound(command.TaskId));

            if (task.Status != TaskStatus.NotStarted && task.Status != TaskStatus.InProgress)
                return Result.Failure(DevelopmentTaskErrors.CannotMarkOverdue(command.TaskId, task.Status));

            task.Status    = TaskStatus.Overdue;
            task.UpdatedAt = dateTimeProvider.UtcNow;

            task.Raise(new TaskOverdueDomainEvent(task.Id, task.GoalId, task.PlanId));

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
