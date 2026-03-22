using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Organizations
{
    public static class OrganizationErrors
    {
        public static Error NotFound(Guid organizationId) => Error.NotFound(
            "Organizations.NotFound",
            $"The organization with the Id = '{organizationId}' was not found");

        public static readonly Error UserAlreadyMember = Error.Conflict(
            "Organizations.UserAlreadyMember",
            "The user is already a member of this organization");

        public static Error UserRoleNotFound(Guid userId, Guid organizationId) => Error.NotFound(
            "Organizations.UserRoleNotFound",
            $"The user '{userId}' has no role in organization '{organizationId}'");

        public static readonly Error InsufficientPermissions = Error.Failure(
            "Organizations.InsufficientPermissions",
            "You do not have sufficient permissions to perform this action");
    }
}
