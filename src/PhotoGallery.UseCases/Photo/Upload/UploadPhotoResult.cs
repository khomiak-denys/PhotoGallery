namespace PhotoGallery.UseCases.Photo.Upload;

public class UploadPhotoResult
{
    public Guid PhotoId { get; init; }
    public string ObjectKey { get; init; } = string.Empty;
    public string UploadUrl { get; init; } = string.Empty;
}
