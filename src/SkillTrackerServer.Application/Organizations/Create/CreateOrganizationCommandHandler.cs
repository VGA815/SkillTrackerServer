using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.Organizations;
using SkillTrackerServer.Domain.Users;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.Organizations.Create
{
    internal sealed class CreateOrganizationCommandHandler(
        IApplicationDbContext context,
        IUserContext userContext,
        IDateTimeProvider dateTimeProvider)
        : ICommandHandler<CreateOrganizationCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateOrganizationCommand command, CancellationToken cancellationToken)
        {
            bool nameExists = await context.Organizations
                .AnyAsync(o => o.Name == command.Name, cancellationToken);

            if (nameExists)
            {
                return Result.Failure<Guid>(OrganizationErrors.NameNotUnique);
            }

            User? user = await context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == userContext.UserId, cancellationToken);

            if (user is null)
            {
                return Result.Failure<Guid>(UserErrors.NotFound(userContext.UserId));
            }

            Organization organization = Organization.Create(
                command.Name,
                command.Description,
                dateTimeProvider.UtcNow);

            UserRole managerRole = UserRole.Create(
                userContext.UserId,
                organization.Id,
                OrganizationRole.Manager,
                dateTimeProvider.UtcNow);

            context.Organizations.Add(organization);
            context.UserRoles.Add(managerRole);

            await context.SaveChangesAsync(cancellationToken);

            return organization.Id;
        }
    }
}
