using Excellerent.ClientManagement.Domain.Entities;
using Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.ClientManagement.Infrastructure.Repositories
{
    public class ClientDetailsRepository : AsyncRepository<ClientDetails>, IClientDetailsRepository
    {
        private readonly EPPContext _context;

        public ClientDetailsRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ClientDetails> GetClientById(Guid id)
        {
            try
            {
                return (await _context.ClientDetails.FindAsync(id));
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<IEnumerable<ClientDetails>> GetClientByName(string clientName)
        {
            try
            {
                IEnumerable<ClientDetails> clientDetails = (await base.GetQueryAsync(x => x.ClientName == clientName)).ToList();
                return clientDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ClientDetails>> GetClientFullData()
        {
            try
            {
                return await _context.ClientDetails.Include(x => x.OperatingAddress)
                                        .Include(x => x.BillingAddress)
                                        .Include(x => x.ClientContacts)
                                         .Include(x => x.ClientStatus)

                                        .Include(x => x.CompanyContacts).AsSplitQuery().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ClientDetails>> GetPaginatedClient(Expression<Func<ClientDetails, bool>> predicate, int pageIndex, int pageSize)
        {
            return predicate == null ? (await _context.ClientDetails.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).Include(x => x.ClientStatus).Include(x => x.OperatingAddress).Include(x => x.BillingAddress).Include(x => x.ClientContacts).Include(x => x.CompanyContacts).AsSplitQuery().ToListAsync())
         : (await _context.ClientDetails.Where(predicate: predicate).OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).Include(x => x.ClientStatus).Include(x => x.OperatingAddress).Include(x => x.BillingAddress).Include(x => x.ClientContacts).Include(x => x.CompanyContacts).AsSplitQuery().ToListAsync());
        }

        
    }
}