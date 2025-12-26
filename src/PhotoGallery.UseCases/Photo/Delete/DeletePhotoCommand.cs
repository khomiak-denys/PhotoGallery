using PhotoGallery.Domain.Common.UserRoles;
using PhotoGallery.UseCases.Abstractions.Messaging;

namespace PhotoGallery.UseCases.Photo.Delete;

public class DeletePhotoCommand : ICommand
{
    public Guid PhotoId { get; init; }
    public Guid UserId { get; init; }
    public UserRoles UserRole { get; init; }
}
