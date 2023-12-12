
using AutoMapper;
using Excellerent.SharedModules.DTO;
using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces;
using Excellerent.UserManagement.Presentation.Models.RequestDtos;
using Excellerent.UserManagement.Presentation.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Excellerent.UserManagement.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class GroupSetController : Controller
    {
        private readonly IGroupSetService _groupSetService;
        private readonly IMapper _mapper;
        private readonly GroupSetPostRequestValidator _validator;
        public GroupSetController(IGroupSetService groupSetService)
        {
            _groupSetService = groupSetService;
            var config = new MapperConfiguration(configuratuion => configuratuion.CreateMap<GroupSetPostRequestDto, GroupSetEntity>().ReverseMap());
            _mapper = new Mapper(config);
            _validator = new GroupSetPostRequestValidator();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> Post(GroupSetPostRequestDto request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseDTO(ResponseStatus.Error, validationResult.ToString(), null));
            }
            return Ok(await _groupSetService.Add(this._mapper.Map<GroupSetEntity>(request)));
        }

        [HttpGet]
        public async  Task<PredicatedResponseDTO> GetAllEmployeeDashboard(string searhKey, int pageIndex, int pageSize)
        {
             pageIndex = pageIndex == 0 ? 1 : pageIndex;
             pageSize = pageSize == 0 ? 10 : pageSize;
            return await  _groupSetService.GetAllUserGroupsDashboardAsync(searhKey, pageIndex, pageSize);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseDTO>> UpdateGroup(GroupSetPostRequestDto request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseDTO(ResponseStatus.Error, validationResult.ToString(), null));
            }
            return Ok(await _groupSetService.Update(this._mapper.Map<GroupSetEntity>(request)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetGroupSet(Guid id)
        {
            var user = await _groupSetService.GetUserById(id);
            return new ResponseDTO(ResponseStatus.Success, "", user);
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseDTO>> DeleteGroup(Guid id)
        {
            //Check wherther there are users or designation assigned to the group
            return Ok(await _groupSetService.Delete(id));
        }
    }
}
