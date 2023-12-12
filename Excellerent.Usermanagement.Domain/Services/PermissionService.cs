
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Seed;
using Excellerent.SharedModules.Services;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces;
using Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces;
using Excellerent.Usermanagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Services
{
    public class PermissionService : CRUD<PermissionEntity, Permission>, IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        public PermissionService(IPermissionRepository permissionRepository) : base(permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public new async Task<ResponseDTO> Add(PermissionEntity entity)
        {
            try
            {
                var model = await _permissionRepository.AddAsync(entity.MapToModel());
                return new ResponseDTO(ResponseStatus.Success, "Successfully added", model.Guid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResponseDTO();
            }

        }

        public async Task<IEnumerable<PermissionEntity>> GetAllPermissions()
        {
            var data = await _permissionRepository.GetAllAsync();
            return data?.Select(x => new PermissionEntity(x));
        }
    }
}
