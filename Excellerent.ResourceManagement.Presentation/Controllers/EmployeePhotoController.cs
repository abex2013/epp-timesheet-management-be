using Excellerent.ResourceManagement.Infrastructure.Dtos;
using Excellerent.ResourceManagement.Presentation.Dtos;
using Excellerent.SharedModules.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Excellerent.ResourceManagement.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class EmployeePhotoController 
    {
        private readonly IWebHostEnvironment _environment;

        public EmployeePhotoController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]
        public ResponseDTO Post([FromForm]EmployeeFileUpload? photo)
        {
            
            if (photo.files != null)
            {
                if (photo.files.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\EmployeesPhoto\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\EmployeesPhoto\\");
                    }
                    using (FileStream fileStream =  System.IO.File.Create(_environment.WebRootPath + "\\EmployeesPhoto\\" + photo.files.FileName))
                    {
                        photo.files.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    return   new ResponseDTO(ResponseStatus.Success, "Entry Succesfull", "\\EmployeesPhoto\\" + photo.files.FileName);

                }
            }
                return  new ResponseDTO(ResponseStatus.Error, "Entry Failed", "\\EmployeesPhoto\\");
           
            
        }
     

    }
}
