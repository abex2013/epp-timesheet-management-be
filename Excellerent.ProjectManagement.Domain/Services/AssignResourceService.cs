using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Domain.Services
{
    public class AssignResourceService : CRUD<AssignResourceEntity, AssignResourcEntity>, IAssignResourceService
    {
        private readonly IAssignResourceRepository _repository;
        public AssignResourceService(IAssignResourceRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public Task<AssignResourcEntity> GetOneAssignResource(Guid id)
        {
            return _repository.GetByGuidAsync(id);
        }

        public Task<IEnumerable<AssignResourcEntity>> GetProjectIdsByEmployee(Guid empId)
        {
            return _repository.GetProjectIdsByEmployee(empId);
            
        }
    }
}
