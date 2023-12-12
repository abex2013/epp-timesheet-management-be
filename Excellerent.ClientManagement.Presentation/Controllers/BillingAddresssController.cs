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
    public class BillingAddresssController : AuthorizedController
    {
        private readonly IBillingAddressService _billingAddress;

        public BillingAddresssController(IHttpContextAccessor htttpContextAccessor, IConfiguration configuration, IBusinessLog _businessLog, IBillingAddressService billingAddress) : base(htttpContextAccessor, configuration, _businessLog, "BillingAddress")
        {
            _billingAddress = billingAddress;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseDTO> AddBillingAddress(BillingAddressPostModel model)
        {
            return await _billingAddress.Add(model.MappToEntity());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ResponseDTO> GetBillAddress()
        {
            return new ResponseDTO
            {
                ResponseStatus = ResponseStatus.Success,
                Data = await _billingAddress.GetAll(),
                Message = "Billing address list",
                Ex = null
            };
        }
    }
}