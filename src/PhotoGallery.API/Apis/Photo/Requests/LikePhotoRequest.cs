using PhotoGallery.UseCases.Photo.Like;

namespace PhotoGallery.API.Apis.Photo.Requests;

public class LikePhotoRequest
{
    public bool IsPositive { get; init; } = true;

    public LikePhotoCommand ToCommand(Guid photoId, Guid userId)
    {
        return new LikePhotoCommand
        {
            PhotoId = photoId,
            UserId = userId,
            IsPositive = IsPositive
        };
    }
}
