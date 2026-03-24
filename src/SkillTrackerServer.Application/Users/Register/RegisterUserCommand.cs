using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Users.Register
{
    public sealed class RegisterUserCommand: ICommand<Guid>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public RegisterUserCommand(string email, string password, string username, string firstName, string? lastName)
        {
            Email = email;
            Password = password;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
