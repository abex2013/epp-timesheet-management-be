
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Services;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces;
using Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces;
using Excellerent.Usermanagement.Domain.Models;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Services
{
    public class GroupSetService : CRUD<GroupSetEntity, GroupSet>, IGroupSetService
    {
        private readonly IGroupSetRepository _groupsetRepository;
        public GroupSetService(IGroupSetRepository groupsetRepository) : base(groupsetRepository)
        {
            _groupsetRepository = groupsetRepository;
        }

        public  async Task<PredicatedResponseDTO> GetAllUserGroupsDashboardAsync(string searchKey, int pageindex, int pageSize)
        {
            var predicate = PredicateBuilder.New<GroupSet>();

            if (searchKey == null)
                predicate = null;
            else
            {
                predicate = predicate.Or(p => p.Name.ToLower().Contains(searchKey.ToLower()))
                                      .Or(p => p.Description.ToLower().Contains(searchKey.ToLower()));
            }

            var result =  _groupsetRepository.GetAllUserGroupsDashboardAsync(predicate, pageindex - 1, pageSize);

            if (result != null)
            {
                List<GroupSet> groupList = (List<GroupSet>)result;
                int totalRowCount =  _groupsetRepository.AllUserGroupsDashboardCountAsync(predicate);
                return new PredicatedResponseDTO
                {
                    Data = groupList,
                    TotalRecord = totalRowCount,//total row count
                    PageIndex = pageindex,
                    PageSize = pageSize,  // itemPerPage,
                    TotalPage = groupList.Count
                };
            }
            else
            {
                return new PredicatedResponseDTO
                {
                    Data = null,
                    TotalRecord = 0,//total row count
                    PageIndex = 0,
                    PageSize = 0,  // itemPerPage,
                    TotalPage = 0
                };
            }
        }

        public new async Task<ResponseDTO> Add(GroupSetEntity entity)
        {
            try
            {
                var model = await _groupsetRepository.AddAsync(entity.MapToModel());
                return new ResponseDTO(ResponseStatus.Success, "Successfully added", model.Guid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResponseDTO();
            }

        }

        public async Task<GroupSetEntity> GetUserById(Guid id)
        {
            try
            {
                GroupSet user = await _groupsetRepository.FindOneAsync(u => u.Guid == id);

                return user == null ? null : new GroupSetEntity(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public new async Task<ResponseDTO> Update(GroupSetEntity entity)
        {
            var data = await _groupsetRepository.FindOneAsync(x => x.Guid == entity.Guid);
            if (data == null)
            {
                return new ResponseDTO(ResponseStatus.Error, "User not found", null);
            }
            var model = entity.MapToModel(data);
            await _groupsetRepository.UpdateAsync(model);
            return new ResponseDTO(ResponseStatus.Success, "Record updated successfully", null);
        }

        public async Task<ResponseDTO> Delete(Guid id)
        {
            var data = await _groupsetRepository.FindOneAsync(x => x.Guid == id);
            if (data == null)
            {
                return new ResponseDTO(ResponseStatus.Error, "User not found", null);
            }
            await _groupsetRepository.DeleteAsync(data);
            return new ResponseDTO(ResponseStatus.Success, "Record deleted successfully", null);
        }
        
    }
}
