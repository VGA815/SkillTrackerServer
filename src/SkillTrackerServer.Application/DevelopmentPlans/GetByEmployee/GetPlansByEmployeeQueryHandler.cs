using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.Organizations;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.DevelopmentPlans.GetByEmployee
{
    internal sealed class GetPlansByEmployeeQueryHandler(
        IApplicationDbContext context,
        IUserContext userContext)
        : IQueryHandler<GetPlansByEmployeeQuery, List<DevelopmentPlanResponse>>
    {
        public async Task<Result<List<DevelopmentPlanResponse>>> Handle(
            GetPlansByEmployeeQuery query,
            CancellationToken cancellationToken)
        {
            bool isMember = await context.UserRoles
                .AnyAsync(ur =>
                    ur.UserId == userContext.UserId &&
                    ur.OrganizationId == query.OrganizationId,
                    cancellationToken);

            if (!isMember)
                return Result.Failure<List<DevelopmentPlanResponse>>(
                    OrganizationErrors.InsufficientPermissions);

            List<DevelopmentPlanResponse> plans = await context.DevelopmentPlans
                .Where(p => p.EmployeeId == userContext.UserId
                         && p.OrganizationId == query.OrganizationId)
                .OrderByDescending(p => p.CreatedAt)
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
                .ToListAsync(cancellationToken);

            return plans;
        }
    }
}
