using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Infrastructure.Repositories
{
    public class ProjectRepository : AsyncRepository<Project>, IProjectRepository
    {
        private readonly EPPContext _context;
        public ProjectRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetProjectByName(string ProjectName)
        {
            try
            {
                IEnumerable<Project> project = (await base.GetQueryAsync(x => x.ProjectName == ProjectName)).ToList();
                return project;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Project> GetProjectById(Guid id)
        {
            try
            {
                return (await _context.Project.FindAsync(id));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<Project>> GetProjectFullData()
        {
            try
            {
                try
                {
                    return _context.Project.Include(x => x.Client).Include(x => x.ProjectStatus).ToList();

                }
                catch (Exception)
                {

                    throw;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Project>> GetPaginatedProject(Expression<Func<Project, bool>> predicate, int pageIndex, int pageSize)
        {
            return predicate == null ? (await _context.Project.OrderByDescending(x=>x.CreatedDate).Skip((pageIndex-1) * pageSize).Take(pageSize).Include(x=>x.Client).Include(y => y.ProjectStatus).ToListAsync())
         : (await _context.Project.Where(predicate: predicate).OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).Include(x => x.Client).Include(y => y.ProjectStatus).ToListAsync());

        }

       
    }
}
