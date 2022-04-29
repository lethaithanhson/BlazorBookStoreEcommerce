using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorEcommerce.Client.Services.FileUploadService
{
    public class FileService : IFileService
    {
        private readonly HttpClient _httpClient;

        public FileService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> UploadFile(ImageFile file)
        {
            var result = await _httpClient.PostAsJsonAsync<ImageFile>("/api/upload",file,System.Threading.CancellationToken.None);
            return result.IsSuccessStatusCode ? true : false;
        }
    }
}
