using SkillTrackerServer.Domain.Users;

namespace SkillTrackerServer.Application.Abstractions.Authentication
{
    public interface ITokenProvider
    {
        string Create(User user);
    }
}
