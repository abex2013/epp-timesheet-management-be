using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.SharedModules.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PersonalAddressController : ControllerBase
    {

        private readonly IPersonalAddressService _personalAddressService;

        public PersonalAddressController(
            IPersonalAddressService personalAddressService
            )
        {
            _personalAddressService = personalAddressService;
        }

        [HttpPost]
        public Task<ResponseDTO> Add(PersonalAddressEntity personalAddress)
        {
            return _personalAddressService.Add(personalAddress);
        }
        [HttpPut]
        [AllowAnonymous]
        public async Task<ResponseDTO> EditPersonalAddress(PersonalAddressEntity personalAddressEntity)
        {
            return await _personalAddressService.Update(personalAddressEntity);
        }

    }

}
