using Excellerent.APIModularization.Controllers;
using Excellerent.APIModularization.Logging;
using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ProjectManagement.Presentation.Models.PostModels;
using Excellerent.SharedModules.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Presentation.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : AuthorizedController
    {
        private readonly IProjectService _projectService;
        private readonly IAssignResourceService _assignResourceService;

        public ProjectController(IHttpContextAccessor htttpContextAccessor, IConfiguration configuration, IBusinessLog _businessLog, IProjectService projectService, IAssignResourceService assignResourceService) : base(htttpContextAccessor, configuration, _businessLog, "Project")
        {
            _projectService = projectService;
            _assignResourceService = assignResourceService;
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<ResponseDTO> Get()
        {
            var data = await _projectService.GetProjectFullData();
            return new ResponseDTO
            {
                Data = data,
                Message = "Project full data list.",
                ResponseStatus = data != null ? ResponseStatus.Success : ResponseStatus.Error,
                Ex = null
            };
        }
        [HttpGet("ProjectById")]
        [AllowAnonymous]
        public async Task<ResponseDTO> GetProjectByID(Guid projectId)
        {
            var data = await _projectService.GetProjectById(projectId);
            return new ResponseDTO
            {
                Data = data,
                Message = "Project by id",
                ResponseStatus = data != null ? ResponseStatus.Success : ResponseStatus.Error,
                Ex = null
            };
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<PredicatedResponseDTO> Get(Guid? id, string searchKey, int? pageindex, int? pageSize)
        {
            return await _projectService.GetPaginatedProject(id, searchKey, pageindex, pageSize);

        }
        [HttpGet("EmployeeProjects(employeeId)")]
        [AllowAnonymous]
        public async Task<ResponseDTO> GetEmployeeProjects(Guid employeeId)
        {
            var projects = await _projectService.GetEmployeeProjects(employeeId);
            return new ResponseDTO
            {
                Data = projects,
                Message = "Project full data list.",
                ResponseStatus = projects != null ? ResponseStatus.Success : ResponseStatus.Error,
                Ex = null
            };

        }

        [HttpGet("EmployeeClient(employeeId)")]
        [AllowAnonymous]
        public async Task<ResponseDTO> GetEmployeeClient(Guid employeeId)
        {
            var client = await _projectService.GetEmployeeClient(employeeId);
            return new ResponseDTO
            {
                Data = client,
                Message = "Client full data list.",
                ResponseStatus = client != null ? ResponseStatus.Success : ResponseStatus.Error,
                Ex = null
            };

        }
        [HttpGet("ProjectClient(projectId)")]
        [AllowAnonymous]
        public async Task<ResponseDTO> GetProjectClient(Guid projectId)
        {
            var client = await _projectService.GetProjectClient(projectId);
            return new ResponseDTO
            {
                Data = client,
                Message = "Client full data list.",
                ResponseStatus = client != null ? ResponseStatus.Success : ResponseStatus.Error,
                Ex = null
            };

        }

        [HttpGet("SupervisorProjects(supervisorGuid)")]
        [AllowAnonymous]
        public async Task<ResponseDTO> Get(Guid supervisorGuid)
        {
            var data = await _projectService.GetAllSupervisorProjects(supervisorGuid);

            return new ResponseDTO
            {
                Data = data,
                Message = "Project full data list.",
                ResponseStatus = data != null ? ResponseStatus.Success : ResponseStatus.Error,
                Ex = null
            };
        }
        [HttpGet("ClientProjects(clientGuid)")]
        [AllowAnonymous]
        public async Task<ResponseDTO> GetClientProjects(Guid clientGuid)
        {
            var data = await _projectService.GetClientProjects(clientGuid);

            return new ResponseDTO
            {
                Data = data,
                Message = "Project full data list.",
                ResponseStatus = data != null ? ResponseStatus.Success : ResponseStatus.Error,
                Ex = null
            };
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseDTO> Add(ProjectPostModel model)
        {
           var data=  await _projectService.Add(model.MappToEntity());
             if(data.ResponseStatus.ToString()== "Success" && model.AssignResource!=null)
            {  
                foreach(var resource in model.AssignResource)
                {
                    resource.ProjectGuid = data.Data.Guid;
                   await _assignResourceService.Add(resource.MappToEntity());
                } 
            }
            return data;
        }
      
        [HttpPut]
        [AllowAnonymous]
        public async Task<ResponseDTO> Edit(ProjectEntity projectEntity)
        {
            return await _projectService.Update(projectEntity);
        }

        [HttpDelete]
        [AllowAnonymous]
        public async Task<ResponseDTO> Remove(ProjectEntity projectEntity)
        {
            return await _projectService.Delete(projectEntity);
        }

    }
}
