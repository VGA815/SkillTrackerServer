using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.Organizations;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.DevelopmentPlans.GetByManager
{
    internal sealed class GetPlansByManagerQueryHandler(
        IApplicationDbContext context,
        IUserContext userContext)
        : IQueryHandler<GetPlansByManagerQuery, List<DevelopmentPlanResponse>>
    {
        public async Task<Result<List<DevelopmentPlanResponse>>> Handle(
            GetPlansByManagerQuery query,
            CancellationToken cancellationToken)
        {
            UserRole? managerRole = await context.UserRoles
                .SingleOrDefaultAsync(ur =>
                    ur.UserId == userContext.UserId &&
                    ur.OrganizationId == query.OrganizationId,
                    cancellationToken);

            if (managerRole is null || managerRole.Role != OrganizationRole.Manager)
                return Result.Failure<List<DevelopmentPlanResponse>>(
                    OrganizationErrors.InsufficientPermissions);

            List<DevelopmentPlanResponse> plans = await context.DevelopmentPlans
                .Where(p => p.ManagerId == userContext.UserId
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
