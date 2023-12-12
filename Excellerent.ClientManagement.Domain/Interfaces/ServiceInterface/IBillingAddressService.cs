using Excellerent.ClientManagement.Domain.Entities;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedModules.Interface.Service;

namespace Excellerent.ClientManagement.Domain.Interfaces.ServiceInterface
{
    public interface IBillingAddressService : ICRUD<BillingAddressEntity, BillingAddress>
    {
    }
}