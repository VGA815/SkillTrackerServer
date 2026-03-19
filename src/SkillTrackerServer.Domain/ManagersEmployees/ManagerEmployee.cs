using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.ManagersEmployees
{
    public sealed class ManagerEmployee : Entity
    {
        public Guid ManagerId { get; private set; }
        public Guid EmployeeId { get; private set; }
        public DateTime LinkedAt { get; private set; }

        public static ManagerEmployee Create(Guid managerId, Guid employeeId, DateTime linkedAt) => new ()
        {
            ManagerId = managerId,
            EmployeeId = employeeId,
            LinkedAt = linkedAt
        };
        public ManagerEmployee()
        {
            
        }
    }
}
