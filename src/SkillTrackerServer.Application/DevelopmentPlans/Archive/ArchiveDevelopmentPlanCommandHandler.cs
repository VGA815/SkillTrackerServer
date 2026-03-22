using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.DevelopmentPlans;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.DevelopmentPlans.Archive
{
    internal sealed class ArchiveDevelopmentPlanCommandHandler(
        IApplicationDbContext context,
        IUserContext userContext,
        IDateTimeProvider dateTimeProvider)
        : ICommandHandler<ArchiveDevelopmentPlanCommand>
    {
        public async Task<Result> Handle(
            ArchiveDevelopmentPlanCommand command,
            CancellationToken cancellationToken)
        {
            DevelopmentPlan? plan = await context.DevelopmentPlans
                .SingleOrDefaultAsync(p => p.Id == command.PlanId, cancellationToken);

            if (plan is null)
                return Result.Failure(DevelopmentPlanErrors.NotFound(command.PlanId));

            if (plan.ManagerId != userContext.UserId)
                return Result.Failure(DevelopmentPlanErrors.AccessDenied);

            if (plan.Status == DevelopmentPlanStatus.Archived)
                return Result.Failure(DevelopmentPlanErrors.AlreadyArchived(command.PlanId));

            plan.Status    = DevelopmentPlanStatus.Archived;
            plan.UpdatedAt = dateTimeProvider.UtcNow;

            plan.Raise(new DevelopmentPlanStatusChangedDomainEvent(plan.Id, DevelopmentPlanStatus.Archived));

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
