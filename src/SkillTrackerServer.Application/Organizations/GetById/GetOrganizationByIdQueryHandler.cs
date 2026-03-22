using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.Organizations;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.Organizations.GetById
{
    internal sealed class GetOrganizationByIdQueryHandler(
        IApplicationDbContext context,
        IUserContext userContext)
        : IQueryHandler<GetOrganizationByIdQuery, OrganizationResponse>
    {
        public async Task<Result<OrganizationResponse>> Handle(
            GetOrganizationByIdQuery query,
            CancellationToken cancellationToken)
        {
            bool isMember = await context.UserRoles
                .AnyAsync(ur => ur.UserId == userContext.UserId && ur.OrganizationId == query.OrganizationId,
                    cancellationToken);

            if (!isMember)
            {
                return Result.Failure<OrganizationResponse>(OrganizationErrors.InsufficientPermissions);
            }

            OrganizationResponse? organization = await context.Organizations
                .Where(o => o.Id == query.OrganizationId)
                .Select(o => new OrganizationResponse
                {
                    Id = o.Id,
                    Name = o.Name,
                    Description = o.Description,
                    CreatedAt = o.CreatedAt,
                    UpdatedAt = o.UpdatedAt
                })
                .SingleOrDefaultAsync(cancellationToken);

            if (organization is null)
            {
                return Result.Failure<OrganizationResponse>(OrganizationErrors.NotFound(query.OrganizationId));
            }

            return organization;
        }
    }
}
