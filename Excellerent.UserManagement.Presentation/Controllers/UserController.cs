using Excellerent.SharedModules.DTO;
using Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.UserManagement.Presentation.Models.GetModels;
using Excellerent.UserManagement.Presentation.Models.PostModel;
using Excellerent.UserManagement.Presentation.Models.PutModels;
using Excellerent.UserManagement.Presentation.Models.Validations;
using Excellerent.UserManagement.Presentation.Validations;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.UserManagement.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]

    [AllowAnonymous]
    public class UserController : Controller
    {
        private IUserService userService { get; }
        private IMapper mapper { get; }
        private UserValidator validator { get; }
        private UserPutModelValidator userPutModelvalidator { get; }
        public UserController(IUserService userService,
            IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
            validator = new UserValidator();
            userPutModelvalidator = new UserPutModelValidator();
        }

        [HttpGet]
        public async Task<ResponseDTO> GetAll()
        {
            var users = await userService.GetUsers();
            var mappedUsers = users.Select(u => mapper.Map<UserGetModel>(u));
            return new ResponseDTO(ResponseStatus.Success, "", mappedUsers);
        }
        [HttpGet("{id}")]
        public async Task<ResponseDTO> Get(Guid id)
        {
            var user = await userService.GetUser(id);
            return new ResponseDTO(ResponseStatus.Success, "", mapper.Map<UserGetModel>(user));
        }

        [HttpPost]
        public async Task<ResponseDTO> Post(UserPostModel user)
        {
            var validator = this.validator.Validate(user);
            if (!validator.IsValid)
                return new ResponseDTO(ResponseStatus.Error, validator.ToString(), null);

            var userExist = await userService.GetUserByEmployeeId(user.EmployeeId);
            if (userExist != null && userExist.Any())
                return new ResponseDTO(ResponseStatus.Error, "Employee already registered as user", null);

            return await userService.Add(mapper.Map<UserEntity>(user));
        }
        [HttpPut]
        public async Task<ResponseDTO> Put(UserPutModel user)
        {
            var validator = this.userPutModelvalidator.Validate(user);
            if (!validator.IsValid)
                return new ResponseDTO(ResponseStatus.Error, validator.ToString(), null);
            return await userService.Update(mapper.Map<UserEntity>(user));
        }

        [HttpGet("GetUsersForDashboard")]
        public async Task<PredicatedResponseDTO> GetAllUsersDashboard(string username, int pageIndex, int pageSize)
        {
            return await userService.GetUserDashBoardList(username, pageIndex, pageSize);
        }
    }
}
