using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task UploadProductImage([FromBody] ImageFile file)
        {
            var buffer = Convert.FromBase64String(file.base64data);
            await System.IO.File.WriteAllBytesAsync(_webHostEnvironment.ContentRootPath + "\\" 
                + Guid.NewGuid().ToString("N") + "-" + file.fileName, buffer);
        }
    }
}
