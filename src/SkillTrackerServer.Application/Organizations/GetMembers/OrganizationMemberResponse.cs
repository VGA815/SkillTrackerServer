using SkillTrackerServer.Domain.Organizations;

namespace SkillTrackerServer.Application.Organizations.GetMembers
{
    public sealed class OrganizationMemberResponse
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string? Position { get; set; }
        public string Email { get; set; } = null!;
        public OrganizationRole Role { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}
