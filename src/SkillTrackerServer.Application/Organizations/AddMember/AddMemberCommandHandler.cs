using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.Organizations;
using SkillTrackerServer.Domain.Users;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.Organizations.AddMember
{
    internal sealed class AddMemberCommandHandler(
        IApplicationDbContext context,
        IUserContext userContext,
        IDateTimeProvider dateTimeProvider)
        : ICommandHandler<AddMemberCommand>
    {
        public async Task<Result> Handle(AddMemberCommand command, CancellationToken cancellationToken)
        {
            UserRole? currentUserRole = await context.UserRoles
                .SingleOrDefaultAsync(
                    ur => ur.UserId == userContext.UserId && ur.OrganizationId == command.OrganizationId,
                    cancellationToken);

            if (currentUserRole is null || currentUserRole.Role != OrganizationRole.Manager)
                return Result.Failure(OrganizationErrors.InsufficientPermissions);

            bool orgExists = await context.Organizations
                .AnyAsync(o => o.Id == command.OrganizationId, cancellationToken);

            if (!orgExists)
                return Result.Failure(OrganizationErrors.NotFound(command.OrganizationId));

            bool targetUserExists = await context.Users
                .AnyAsync(u => u.Id == command.UserId, cancellationToken);

            if (!targetUserExists)
                return Result.Failure(UserErrors.NotFound(command.UserId));

            bool alreadyMember = await context.UserRoles
                .AnyAsync(
                    ur => ur.UserId == command.UserId && ur.OrganizationId == command.OrganizationId,
                    cancellationToken);

            if (alreadyMember)
                return Result.Failure(OrganizationErrors.UserAlreadyMember);

            UserRole userRole = UserRole.Create(
                command.UserId,
                command.OrganizationId,
                command.Role,
                dateTimeProvider.UtcNow);

            context.UserRoles.Add(userRole);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
