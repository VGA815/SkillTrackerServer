using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.DevelopmentPlans;
using SkillTrackerServer.Domain.Organizations;
using SkillTrackerServer.Domain.Users;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.DevelopmentPlans.Create
{
    internal sealed class CreateDevelopmentPlanCommandHandler(
        IApplicationDbContext context,
        IUserContext userContext,
        IDateTimeProvider dateTimeProvider)
        : ICommandHandler<CreateDevelopmentPlanCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(
            CreateDevelopmentPlanCommand command,
            CancellationToken cancellationToken)
        {
            // Caller must be a Manager in this organization
            UserRole? managerRole = await context.UserRoles
                .SingleOrDefaultAsync(
                    ur => ur.UserId == userContext.UserId
                       && ur.OrganizationId == command.OrganizationId,
                    cancellationToken);

            if (managerRole is null || managerRole.Role != OrganizationRole.Manager)
                return Result.Failure<Guid>(OrganizationErrors.InsufficientPermissions);

            // Employee must exist and belong to the same organization
            bool employeeExists = await context.Users
                .AnyAsync(u => u.Id == command.EmployeeId, cancellationToken);

            if (!employeeExists)
                return Result.Failure<Guid>(UserErrors.NotFound(command.EmployeeId));

            bool employeeInOrg = await context.UserRoles
                .AnyAsync(
                    ur => ur.UserId == command.EmployeeId
                       && ur.OrganizationId == command.OrganizationId,
                    cancellationToken);

            if (!employeeInOrg)
                return Result.Failure<Guid>(OrganizationErrors.UserRoleNotFound(command.EmployeeId, command.OrganizationId));

            DevelopmentPlan plan = DevelopmentPlan.Create(
                managerId:      userContext.UserId,
                employeeId:     command.EmployeeId,
                organizationId: command.OrganizationId,
                title:          command.Title,
                description:    command.Description,
                startDate:      command.StartDate,
                endDate:        command.EndDate,
                createdAt:      dateTimeProvider.UtcNow);

            plan.Raise(new DevelopmentPlanCreatedDomainEvent(
                plan.Id, userContext.UserId, command.EmployeeId, command.OrganizationId));

            context.DevelopmentPlans.Add(plan);
            await context.SaveChangesAsync(cancellationToken);

            return plan.Id;
        }
    }
}
