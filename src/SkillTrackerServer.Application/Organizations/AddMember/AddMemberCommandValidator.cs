using FluentValidation;

namespace SkillTrackerServer.Application.Organizations.AddMember
{
    internal sealed class AddMemberCommandValidator : AbstractValidator<AddMemberCommand>
    {
        public AddMemberCommandValidator()
        {
            RuleFor(c => c.OrganizationId).NotEmpty();
            RuleFor(c => c.UserId).NotEmpty();
            RuleFor(c => c.Role).IsInEnum();
        }
    }
}
