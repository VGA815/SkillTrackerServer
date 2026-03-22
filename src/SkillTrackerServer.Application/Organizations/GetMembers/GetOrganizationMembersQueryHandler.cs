using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.Organizations;
using SkillTrackerServer.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace SkillTrackerServer.Application.Organizations.GetMembers
{
    internal sealed class GetOrganizationMembersQueryHandler(
        IApplicationDbContext context,
        IUserContext userContext)
        : IQueryHandler<GetOrganizationMembersQuery, List<OrganizationMemberResponse>>
    {
        public async Task<Result<List<OrganizationMemberResponse>>> Handle(
            GetOrganizationMembersQuery query,
            CancellationToken cancellationToken)
        {
            bool isMember = await context.UserRoles
                .AnyAsync(ur => ur.UserId == userContext.UserId && ur.OrganizationId == query.OrganizationId,
                    cancellationToken);

            if (!isMember)
            {
                return Result.Failure<List<OrganizationMemberResponse>>(OrganizationErrors.InsufficientPermissions);
            }

            bool orgExists = await context.Organizations
                .AnyAsync(o => o.Id == query.OrganizationId, cancellationToken);

            if (!orgExists)
            {
                return Result.Failure<List<OrganizationMemberResponse>>(OrganizationErrors.NotFound(query.OrganizationId));
            }

            List<OrganizationMemberResponse> members = await context.UserRoles
                .Where(ur => ur.OrganizationId == query.OrganizationId)
                .Join(context.Users,
                    ur => ur.UserId,
                    u => u.Id,
                    (ur, u) => new OrganizationMemberResponse
                    {
                        UserId = u.Id,
                        Username = u.Username,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Position = u.Position,
                        Email = u.Email,
                        Role = ur.Role,
                        AssignedAt = ur.AssignedAt
                    })
                .OrderBy(m => m.Role)
                .ThenBy(m => m.Username)
                .ToListAsync(cancellationToken);

            return members;
        }
    }
}
