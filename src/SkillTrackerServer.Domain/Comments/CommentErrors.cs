using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Comments
{
    public static class CommentErrors
    {
        public static Error NotFound(Guid commentId) => Error.NotFound(
            "Comments.NotFound",
            $"The comment with the Id = '{commentId}' was not found");

        public static readonly Error CannotEditOthersComment = Error.Failure(
            "Comments.CannotEditOthersComment",
            "You can only edit your own comments");

        public static readonly Error CannotDeleteOthersComment = Error.Failure(
            "Comments.CannotDeleteOthersComment",
            "You can only delete your own comments unless you are a manager");

        public static readonly Error DoesNotBelongToTask = Error.Problem(
            "Comments.DoesNotBelongToTask",
            "The specified comment does not belong to this task");
    }
}
