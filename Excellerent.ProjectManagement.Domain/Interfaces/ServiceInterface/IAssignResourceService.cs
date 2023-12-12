using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.Interface.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface
{
    public interface IAssignResourceService : ICRUD<AssignResourceEntity, AssignResourcEntity>
    {
        public Task<AssignResourcEntity> GetOneAssignResource(Guid id);
       Task<IEnumerable<AssignResourcEntity>> GetProjectIdsByEmployee(Guid empId);

    }
}
