using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface
{
    public interface IAssignResourceRepository : IAsyncRepository<AssignResourcEntity>
    {
       Task <IEnumerable<AssignResourcEntity>> GetProjectIdsByEmployee(Guid empId);
    }
}
