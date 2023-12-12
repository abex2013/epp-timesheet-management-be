using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces;
using Excellerent.Usermanagement.Domain.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Infrastructure.Repositories
{
    public class GroupSetPermissionRepository : AsyncRepository<GroupSetPermission>, IGroupSetPermissionRepository
    {
        private readonly EPPContext _context;

        public GroupSetPermissionRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Permission>> GetPermissionByGroupId(Guid guid)
        {
            return await _context.GroupSetPermissions.Include(x => x.Permission)
                                .Where(entry => entry.GroupSetId == guid)
                                .Select(entry => entry.Permission).ToListAsync();
        }    
    }    
}
