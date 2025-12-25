using PhotoGallery.UseCases.Abstractions.Messaging;

namespace PhotoGallery.UseCases.Photo.Like;

public class LikePhotoCommand : ICommand
{
    public Guid PhotoId { get; init; }
    public Guid UserId { get; init; }
    public bool IsPositive { get; init; } = true;
}
