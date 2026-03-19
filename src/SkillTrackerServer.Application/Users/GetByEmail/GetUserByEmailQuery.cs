using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Users.GetByEmail
{
    public sealed record GetUserByEmailQuery(string Email) : IQuery<UserResponse>;
}
