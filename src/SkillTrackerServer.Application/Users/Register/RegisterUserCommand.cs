using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.Users;

namespace SkillTrackerServer.Application.Users.Register
{
    public sealed class RegisterUserCommand: ICommand<Guid>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public UserRole UserRole { get; set; }

        public RegisterUserCommand(string email, string password, string username, string firstName, string? lastName, UserRole userRole)
        {
            Email = email;
            Password = password;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            UserRole = userRole;
        }
    }
}
