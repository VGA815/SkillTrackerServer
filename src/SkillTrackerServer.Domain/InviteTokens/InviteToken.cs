using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.InviteTokens
{
    public sealed class InviteToken : Entity
    {
        public Guid Id { get; set; }
        public Guid ManagerId { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }

        public static InviteToken Create(Guid managerId, DateTime expiresAt)
        {
            return new InviteToken
            {
                Id = Guid.NewGuid(),
                ManagerId = managerId,
                ExpiresAt = expiresAt,
                IsUsed = false
            };
        }
        public InviteToken()
        {
            
        }
    }
}
