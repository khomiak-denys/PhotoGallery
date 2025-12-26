namespace PhotoGallery.UseCases.Abstractions.Services;

public interface IObjectStorageService
{
    Task<string> GetFileUrl(string objectKey, TimeSpan? expires = null);
    Task<string> GetUploadUrl(string objectKey, TimeSpan? expires = null);
}
