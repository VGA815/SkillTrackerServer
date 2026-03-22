using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Comments
{
    public sealed class Comment : Entity
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public Guid AuthorId { get; set; }
        public string Body { get; set; } = null!;
        public bool IsEdited { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public static Comment Create(Guid taskId, Guid authorId, string body, DateTime createdAt)
        {
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                TaskId = taskId,
                AuthorId = authorId,
                Body = body,
                IsEdited = false,
                CreatedAt = createdAt,
                UpdatedAt = createdAt
            };

            return comment;
        }

        public Comment() { }
    }
}
