using Excellerent.APIModularization.Controllers;
using Excellerent.APIModularization.Logging;
using Excellerent.ClientManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ClientManagement.Presentation.Models.PostModels;
using Excellerent.SharedModules.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Excellerent.ClientManagement.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientDetailsController : AuthorizedController
    {
        private readonly IClientDetailsService _clientDetailsService;

        public ClientDetailsController(IHttpContextAccessor htttpContextAccessor, IConfiguration configuration, IBusinessLog _businessLog, IClientDetailsService clientDetailsService) : base(htttpContextAccessor, configuration, _businessLog, "ClientDetails")
        {
            _clientDetailsService = clientDetailsService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ResponseDTO> Get()
        {
            return await _clientDetailsService.GetClientFullData();
        }
        [HttpGet("SelectClientById")]
        [AllowAnonymous]
        public async Task<ResponseDTO> GetClientById(Guid clientId)
        {
            var data = await _clientDetailsService.GetClientById(clientId);

            return new ResponseDTO
            {
                Data = data,
                Message = "A single client details.",
                ResponseStatus = data != null ? ResponseStatus.Success : ResponseStatus.Error,
                Ex = null
            };
        }

        [HttpGet("Predicated")]
        [AllowAnonymous]
        public async Task<PredicatedResponseDTO> Get(Guid? id, string searchKey, int? pageindex, int? pageSize)
        {
            return await _clientDetailsService.GetPaginatedClient(id, searchKey, pageindex, pageSize);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseDTO> Add(ClientPostModel model)
        {
            return await _clientDetailsService.AddNewClient(model.MappToEntity());
        }
    }
}