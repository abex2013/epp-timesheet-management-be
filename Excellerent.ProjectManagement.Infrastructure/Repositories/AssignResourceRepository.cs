using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Infrastructure.Repositories
{
    public class AssignResourceRepository : AsyncRepository<AssignResourcEntity>, IAssignResourceRepository
    {
        private readonly EPPContext _context;
        public AssignResourceRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AssignResourcEntity>> GetProjectIdsByEmployee(Guid empId)
        {
            return (await _context.AssignResources.Where(e => e.EmployeeGuid.Equals(empId)).ToListAsync());
        }
    }
}
