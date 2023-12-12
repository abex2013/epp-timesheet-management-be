using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface
{
    public interface IProjectRepository : IAsyncRepository<Project>
    {
        Task<IEnumerable<Project>> GetProjectByName(string ProjectName);
        Task<Project> GetProjectById(Guid id);
        Task<IEnumerable<Project>> GetProjectFullData();
        Task<IEnumerable<Project>> GetPaginatedProject(Expression<Func<Project, Boolean>> predicate, int pageIndex, int pageSize);
    }
}
