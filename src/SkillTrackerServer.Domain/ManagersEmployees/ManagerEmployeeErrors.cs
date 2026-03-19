using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.ManagersEmployees
{
    public static class ManagerEmployeeErrors
    {
        public static Error NotFound(Guid managerId, Guid employeeId) => Error.NotFound(
            "ManagersEmployees.NotFound",
            $"The manager employee with the managerId = '{managerId}' and employeeId = '{employeeId}' was not found");
    }
}
