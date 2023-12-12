
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces;
using Excellerent.Usermanagement.Domain.Models;

namespace Excellerent.Usermanagement.Infrastructure.Repositories
{
    public class PermissionRepository : AsyncRepository<Permission>, IPermissionRepository
    {
        private readonly EPPContext _context;

        public PermissionRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

    }
}
