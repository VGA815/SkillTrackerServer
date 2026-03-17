using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Users
{
    public sealed class User : Entity
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsVerified { get; set; }
        public string PasswordHash { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public void Verify() => IsVerified = true;
        public static User Create(string username, string email, string passwordHash, DateTime createdAt, DateTime updatedAt) =>
            new () { Id = Guid.NewGuid(), Username = username, Email = email, PasswordHash = passwordHash, CreatedAt = createdAt, UpdatedAt = updatedAt, IsVerified = false};
        public User() { }
    }
}
