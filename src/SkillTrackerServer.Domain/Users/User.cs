using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Users
{
    public sealed class User : Entity
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public UserRole UserRole { get; set; }
        public string? Position { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public bool IsVerified { get; set; }
        public string PasswordHash { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public void Verify() => IsVerified = true;
        public static User Create(string username, string firstName, string? lastName, string email, string passwordHash, UserRole userRole, DateTime createdAt, string? position = null)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = passwordHash,
                UserRole = userRole,
                Position = position,
                IsVerified = false,
                CreatedAt = createdAt,
                UpdatedAt = createdAt
            };
        }
        public User() { }
    }
}
