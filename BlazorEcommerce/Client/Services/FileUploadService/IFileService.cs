namespace BlazorEcommerce.Client.Services.FileUploadService
{
    public interface IFileService
    {
        Task<bool> UploadFile(ImageFile file);
    }
}
