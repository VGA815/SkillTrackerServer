using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.Users;
using SkillTrackerServer.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace SkillTrackerServer.Application.Users.GetByEmail
{
    internal sealed class GetUserByEmailQueryHandler(IApplicationDbContext context, IUserContext userContext)
        : IQueryHandler<GetUserByEmailQuery, UserResponse>
    {
        public async Task<Result<UserResponse>> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
        {
            UserResponse? user = await context.Users
                .Where(u => u.Email == query.Email)
                .Select(u =>  new UserResponse
                {
                    Id = u.Id,
                    Email = u.Email,
                    Username = u.Username,
                })
                .SingleOrDefaultAsync(cancellationToken);
            
            if (user is null)
            {
                return Result.Failure<UserResponse>(UserErrors.NotFoundByEmail);
            }
            if (user.Id != userContext.UserId)
            {
                return Result.Failure<UserResponse>(UserErrors.Unauthorized());
            }
            return user;
        }
    }
}
