using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.UserPreferences.Delete
{
    public sealed record DeleteUserPreferenceCommand(Guid UserId) : ICommand;
}
