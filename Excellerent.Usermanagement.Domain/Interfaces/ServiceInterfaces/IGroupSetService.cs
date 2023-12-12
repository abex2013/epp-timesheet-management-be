
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Interface.Service;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Models;
using System.Threading.Tasks;
using System;

namespace Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces
{
    public interface IGroupSetService : ICRUD<GroupSetEntity, GroupSet>
    {
        Task<PredicatedResponseDTO> GetAllUserGroupsDashboardAsync(string searchKey, int pageindex, int pageSize);

        Task<GroupSetEntity> GetUserById(Guid id);

        new Task<ResponseDTO> Update(GroupSetEntity entity);

        Task<ResponseDTO> Delete(Guid id);
    }
}
