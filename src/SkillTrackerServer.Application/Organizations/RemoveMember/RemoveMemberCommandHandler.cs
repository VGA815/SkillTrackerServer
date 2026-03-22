using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.Organizations;
using SkillTrackerServer.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace SkillTrackerServer.Application.Organizations.RemoveMember
{
    internal sealed class RemoveMemberCommandHandler(
        IApplicationDbContext context,
        IUserContext userContext)
        : ICommandHandler<RemoveMemberCommand>
    {
        public async Task<Result> Handle(RemoveMemberCommand command, CancellationToken cancellationToken)
        {
            UserRole? currentUserRole = await context.UserRoles
                .SingleOrDefaultAsync(
                    ur => ur.UserId == userContext.UserId && ur.OrganizationId == command.OrganizationId,
                    cancellationToken);

            if (currentUserRole is null || currentUserRole.Role != OrganizationRole.Manager)
                return Result.Failure(OrganizationErrors.InsufficientPermissions);

            if (command.UserId == userContext.UserId)
                return Result.Failure(OrganizationErrors.CannotRemoveSelf);

            UserRole? targetRole = await context.UserRoles
                .SingleOrDefaultAsync(
                    ur => ur.UserId == command.UserId && ur.OrganizationId == command.OrganizationId,
                    cancellationToken);

            if (targetRole is null)
                return Result.Failure(OrganizationErrors.UserRoleNotFound(command.UserId, command.OrganizationId));

            context.UserRoles.Remove(targetRole);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
