using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Services;

namespace Excellerent.ResourceManagement.Domain.Services
{
    public class PersonalAddressService :
        CRUD<PersonalAddressEntity, PersonalAddress>,
        IPersonalAddressService
    {
        public PersonalAddressService(IAsyncPersonalAddressRepository repository) : base(repository)
        {

        }
    }
}
