using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Seed;
using Excellerent.SharedModules.Services;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces;
using Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces;
using Excellerent.Usermanagement.Domain.Models;
using LinqKit;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Services
{
    public class UserService : CRUD<UserEntity, User>, IUserService
    {
        public IUserRepository _repository { get; }
        public UserService(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<PredicatedResponseDTO> GetUserDashBoardList(string userName, int pageIndex, int pageSize)
        {
            var predicate = PredicateBuilder.True<User>();

            if (userName == "" || userName == null)
                predicate = null;
            else
            {
                predicate = predicate.And(p => p.UserName.ToLower().Contains(userName.ToLower()));
            }

            var result = await _repository.GetUserDashBoardList(predicate, pageIndex - 1, pageSize);
            if (result != null)
            {
                int totalRowCount = await _repository.GetUserDashBoardListCount(predicate);
                return new PredicatedResponseDTO
                {
                    Data = result,
                    TotalRecord = totalRowCount,//total row count
                    PageIndex = pageIndex,
                    PageSize = pageSize,  // itemPerPage,
                    TotalPage = result.Count
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

        public async Task<IEnumerable<UserEntity>> GetUsers()
        {
            try
            {
                IEnumerable<User> users = await _repository.GetAllAsync();
                IEnumerable<UserEntity> mappedUsers = new List<UserEntity>() ;
                if(users.Any())
                mappedUsers = users.Select(u => new UserEntity(u));
                return mappedUsers;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<UserEntity> GetUser(Guid id)
        {
            try
            {
                User user = await _repository.FindOneAsync(u => u.Guid == id);

                return user ==null ? null:  new UserEntity(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public async Task<IEnumerable<User>> GetUserByEmployeeId(Guid empId)
        {
            try
            {
                return await _repository.FindAsync(u => u.EmployeeId == empId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public new async Task<ResponseDTO> Update(UserEntity entity)
        {
            var data = await _repository.FindOneAsync(x => x.Guid == entity.Guid);
            if (data == null)
            {
                return new ResponseDTO(ResponseStatus.Error, "User not found", null);
            }
            var model = entity.MapToModel(data);
            await _repository.UpdateAsync(model);
            return new ResponseDTO(ResponseStatus.Success, "Record updated successfully", null);
        }
    }
}
