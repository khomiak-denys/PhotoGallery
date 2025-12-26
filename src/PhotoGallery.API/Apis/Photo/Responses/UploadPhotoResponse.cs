namespace PhotoGallery.API.Apis.Responses;

public class UploadPhotoResponse
{
    public Guid PhotoId { get; init; }
    public string ObjectKey { get; init; } = string.Empty;
    public string UploadUrl { get; init; } = string.Empty;
}
