using FluentValidation;

namespace SkillTrackerServer.Application.Organizations.RemoveMember
{
    internal sealed class RemoveMemberCommandValidator : AbstractValidator<RemoveMemberCommand>
    {
        public RemoveMemberCommandValidator()
        {
            RuleFor(c => c.OrganizationId).NotEmpty();
            RuleFor(c => c.UserId).NotEmpty();
        }
    }
}
