using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.UserPreferences;

namespace SkillTrackerServer.Application.UserPreferences.Create
{
    public sealed class CreateUserPreferenceCommand : ICommand<Guid>
    {
        public Guid UserId { get; set; }
        public bool ReceiveNotifications { get; set; }
        public UserPreferenceTheme Theme { get; set; }
    }
}
