using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Organizations
{
    public sealed class UserRole : Entity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid OrganizationId { get; set; }
        public OrganizationRole Role { get; set; }
        public DateTime AssignedAt { get; set; }

        public static UserRole Create(Guid userId, Guid organizationId, OrganizationRole role, DateTime assignedAt) =>
            new()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                OrganizationId = organizationId,
                Role = role,
                AssignedAt = assignedAt
            };

        public UserRole() { }
    }
}
