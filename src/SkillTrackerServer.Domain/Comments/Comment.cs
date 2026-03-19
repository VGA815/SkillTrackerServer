using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Comments
{
    public sealed class Comment : Entity
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public Guid AuthorId { get; set; }
        public string Content { get; set; } = null!;
        public static Comment Create(Guid taskId, Guid authorId, string content)
        {
            return new Comment
            {
                Id = Guid.NewGuid(),
                TaskId = taskId,
                AuthorId = authorId,
                Content = content
            };
        }
        public Comment()
        {
            
        }
    }
}
