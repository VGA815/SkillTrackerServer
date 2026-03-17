using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Notifications
{
    public sealed class Notification : Entity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Type { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public Guid? ReferenceId { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public static Notification Create(Guid userId, string type, string title, string body, DateTime createdAt, Guid? referenceId = null) => new ()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Type = type,
            Title = title,
            Body = body,
            ReferenceId = referenceId,
            IsRead = false,
            CreatedAt = createdAt
        };
        public void MarkAsRead()
        {
            IsRead = true;
        }
        public Notification() { }
    }
}
