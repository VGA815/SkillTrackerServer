using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.DevelopmentPlans;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.DevelopmentPlans.GetById
{
    internal sealed class GetDevelopmentPlanByIdQueryHandler(
        IApplicationDbContext context,
        IUserContext userContext)
        : IQueryHandler<GetDevelopmentPlanByIdQuery, DevelopmentPlanResponse>
    {
        public async Task<Result<DevelopmentPlanResponse>> Handle(
            GetDevelopmentPlanByIdQuery query,
            CancellationToken cancellationToken)
        {
            DevelopmentPlanResponse? plan = await context.DevelopmentPlans
                .Where(p => p.Id == query.PlanId)
                .Select(p => new DevelopmentPlanResponse
                {
                    Id           = p.Id,
                    ManagerId    = p.ManagerId,
                    EmployeeId   = p.EmployeeId,
                    OrganizationId = p.OrganizationId,
                    Title        = p.Title,
                    Description  = p.Description,
                    Status       = p.Status,
                    StartDate    = p.StartDate,
                    EndDate      = p.EndDate,
                    CreatedAt    = p.CreatedAt,
                    UpdatedAt    = p.UpdatedAt,
                })
                .SingleOrDefaultAsync(cancellationToken);

            if (plan is null)
                return Result.Failure<DevelopmentPlanResponse>(
                    DevelopmentPlanErrors.NotFound(query.PlanId));

            // Access: only the manager or employee of this plan
            bool hasAccess = plan.ManagerId == userContext.UserId
                          || plan.EmployeeId == userContext.UserId;

            if (!hasAccess)
                return Result.Failure<DevelopmentPlanResponse>(DevelopmentPlanErrors.AccessDenied);

            return plan;
        }
    }
}
