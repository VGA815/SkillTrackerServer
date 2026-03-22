using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.DevelopmentPlans;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.DevelopmentPlans.Activate
{
    internal sealed class ActivateDevelopmentPlanCommandHandler(
        IApplicationDbContext context,
        IUserContext userContext,
        IDateTimeProvider dateTimeProvider)
        : ICommandHandler<ActivateDevelopmentPlanCommand>
    {
        public async Task<Result> Handle(
            ActivateDevelopmentPlanCommand command,
            CancellationToken cancellationToken)
        {
            DevelopmentPlan? plan = await context.DevelopmentPlans
                .SingleOrDefaultAsync(p => p.Id == command.PlanId, cancellationToken);

            if (plan is null)
                return Result.Failure(DevelopmentPlanErrors.NotFound(command.PlanId));

            if (plan.ManagerId != userContext.UserId)
                return Result.Failure(DevelopmentPlanErrors.AccessDenied);

            if (plan.Status != DevelopmentPlanStatus.Draft)
                return Result.Failure(DevelopmentPlanErrors.CannotActivate(command.PlanId, plan.Status));

            plan.Status    = DevelopmentPlanStatus.Active;
            plan.UpdatedAt = dateTimeProvider.UtcNow;

            plan.Raise(new DevelopmentPlanStatusChangedDomainEvent(plan.Id, DevelopmentPlanStatus.Active));

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
