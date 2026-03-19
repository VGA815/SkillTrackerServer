using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.Users;
using SkillTrackerServer.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace SkillTrackerServer.Application.Users.GetById
{
    internal sealed class GetUserByIdQueryHandler(IApplicationDbContext context, IUserContext userContext)
        : IQueryHandler<GetUserByIdQuery, UserResponse>
    {
        public async Task<Result<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            if (query.UserId !=  userContext.UserId)
            {
                return Result.Failure<UserResponse>(UserErrors.Unauthorized());
            }

            UserResponse? user = await context.Users
                .Where(u => u.Id == query.UserId)
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                })
                .SingleOrDefaultAsync(cancellationToken);

            if (user is null)
            {
                return Result.Failure<UserResponse>(UserErrors.NotFound(query.UserId));
            }

            return user;
        }
    }
}
