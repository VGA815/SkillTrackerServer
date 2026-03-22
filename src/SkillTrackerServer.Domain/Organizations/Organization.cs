using SkillTrackerServer.Domain.Users;
using SkillTrackerServer.SharedKernel;
using System;
using System.Collections.Generic;

namespace SkillTrackerServer.Domain.Organizations
{
    public sealed class Organization : Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public static Organization Create(string name, string? description, DateTime createdAt) =>
            new()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                CreatedAt = createdAt,
                UpdatedAt = createdAt
            };

        public Organization() { }
    }
}
