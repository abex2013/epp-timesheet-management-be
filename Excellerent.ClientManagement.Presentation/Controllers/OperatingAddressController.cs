using Excellerent.APIModularization.Controllers;
using Excellerent.APIModularization.Logging;
using Excellerent.ClientManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ClientManagement.Presentation.Models.PostModels;
using Excellerent.SharedModules.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Excellerent.ClientManagement.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class OperatingAddressController : AuthorizedController
    {
        private readonly IOperatingAddressService _operatingAddress;

        public OperatingAddressController(IHttpContextAccessor htttpContextAccessor, IConfiguration configuration, IBusinessLog _businessLog, IOperatingAddressService operatingAddress) : base(htttpContextAccessor, configuration, _businessLog, "BillingAddress")
        {
            _operatingAddress = operatingAddress;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseDTO> AddOperatingAddress(OperatingAddressPostModel model)
        {
            return await _operatingAddress.Add(model.MappToEntity());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ResponseDTO> GetOperatingAddress()
        {
            return new ResponseDTO
            {
                ResponseStatus = ResponseStatus.Success,
                Data = await _operatingAddress.GetAll(),
                Message = "Operating address list",
                Ex = null
            };
        }
    }
}