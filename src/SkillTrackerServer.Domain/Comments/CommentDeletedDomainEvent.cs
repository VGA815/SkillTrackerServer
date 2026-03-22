using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Comments
{
    public sealed record CommentDeletedDomainEvent(Guid CommentId, Guid TaskId, Guid AuthorId) : IDomainEvent;
}
