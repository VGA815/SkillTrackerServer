using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Users.Login
{
    public sealed record LoginUserCommand(string Email, string Password) : ICommand<string>;
}
