using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Users
{
    public static class UserErrors
    {
        public static Error NotFound(Guid userId) => Error.NotFound(
            "Users.NotFound",
            $"The user with the Id = '{userId}' was not found");
        public static Error Unauthorized() => Error.Failure(
            "Users.Unauthorized",
            "You are not authorized to perform this action");
        public static readonly Error NotFoundByEmail = Error.NotFound(
            "Users.NotFoundByEmail",
            "The user with the specified email was not found");
        public static readonly Error EmailNotUnique = Error.Conflict(
            "Users.EmailNotUnique",
            "The provided email is not unique");
        public static readonly Error UsernameNotUnique = Error.Conflict(
            "Users.UsernameNotUnique",
            "The provided username is not unique");
        public static readonly Error NotFoundByUsername = Error.NotFound(
            "Users.NotFoundByUsername",
            "The user with the specified username was not found");
    }
}
