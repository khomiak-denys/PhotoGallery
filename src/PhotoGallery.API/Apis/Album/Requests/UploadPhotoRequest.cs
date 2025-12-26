using PhotoGallery.Domain.Common.UserRoles;
using PhotoGallery.UseCases.Photo.Upload;

namespace PhotoGallery.API.Apis.Album.Requests;

public class UploadPhotoRequest
{
    public string FileName { get; init; } = string.Empty;
    public string? ContentType { get; init; }

    public UploadPhotoCommand ToCommand(Guid albumId, Guid userId, UserRoles userRole)
    {
        return new UploadPhotoCommand
        {
            AlbumId = albumId,
            UserId = userId,
            UserRole = userRole,
            FileName = FileName,
            ContentType = ContentType
        };
    }
}
