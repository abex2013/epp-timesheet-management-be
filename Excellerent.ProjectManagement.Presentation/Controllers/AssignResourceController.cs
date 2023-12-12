using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.ProjectManagement.Presentation.Models.PostModels;
using Excellerent.SharedModules.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class AssignResourceController : ControllerBase
    {
        private readonly IAssignResourceService _assignResourceService;

        public AssignResourceController(IAssignResourceService assignResourceService)
        {
            _assignResourceService = assignResourceService;
        }
        [HttpPost]
        [AllowAnonymous]
        public ResponseDTO AddAssignResource(AssignResourcePostModel model)
        {
            return _assignResourceService.Add(model.MappToEntity()).Result;


        }
        [HttpGet]
        [AllowAnonymous]
        public Task<IEnumerable<AssignResourcEntity>> GetAssignResource()
        {
            return _assignResourceService.GetAll();


        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public AssignResourcEntity GetAssignResourceById(Guid id)
        {
            return _assignResourceService.GetOneAssignResource(id).Result;
        }

        [HttpPut]
        [AllowAnonymous]
        public ResponseDTO UpdateAssignResource(AssignResourcePostModel modelItem)
        {

            return _assignResourceService.Update(modelItem.MappToEntity()).Result;


        }

        [HttpDelete]
        [AllowAnonymous]
        public ResponseDTO DeleteAssignResource(Guid id)
        {
            AssignResourceEntity assignResourceEntity = new AssignResourceEntity();
            assignResourceEntity.Guid = id;
            return _assignResourceService.Delete(assignResourceEntity).Result;


        }

    }
}
