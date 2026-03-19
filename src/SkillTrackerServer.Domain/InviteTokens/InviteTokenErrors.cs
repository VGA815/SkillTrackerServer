using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.InviteTokens
{
    public static class InviteTokenErrors
    {
        public static Error NotFound(Guid tokenId) => Error.NotFound(
            "InviteToken.NotFound",
            $"The invite token with the id = '{tokenId}' was not found.");
    }
}
