using PhotoGallery.Domain.Common.UserRoles;
using PhotoGallery.UseCases.Abstractions.Messaging;

namespace PhotoGallery.UseCases.Photo.Upload;

public class UploadPhotoCommand : ICommand<UploadPhotoResult>
{
    public Guid AlbumId { get; init; }
    public Guid UserId { get; init; }
    public UserRoles UserRole { get; init; }
    public string FileName { get; init; } = string.Empty;
}
