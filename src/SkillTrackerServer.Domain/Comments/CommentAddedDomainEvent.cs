using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Comments
{
    public sealed record CommentAddedDomainEvent(Guid CommentId, Guid TaskId, Guid AuthorId) : IDomainEvent;
}
