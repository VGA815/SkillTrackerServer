using SkillTrackerServer.Domain.UserPreferences;

namespace SkillTrackerServer.Application.UserPreferences.GetById
{
    public class UserPreferenceResponse
    {
        public Guid UserId { get; set; }
        public UserPreferenceTheme Theme { get; set; }
        public bool ReceiveNotifications { get; set; }
    }
}