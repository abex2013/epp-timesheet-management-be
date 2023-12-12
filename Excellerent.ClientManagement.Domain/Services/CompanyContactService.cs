using Excellerent.ClientManagement.Domain.Entities;
using Excellerent.ClientManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using Excellerent.SharedModules.Services;

namespace Excellerent.ClientManagement.Domain.Services
{
    public class CompanyContactService : CRUD<CompanyContactEntity, CompanyContact>, ICompanyContactService
    {
        public CompanyContactService(IAsyncRepository<CompanyContact> repository) : base(repository)
        {
        }
    }
}