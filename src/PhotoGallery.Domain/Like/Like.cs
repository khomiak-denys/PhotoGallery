using PhotoGallery.Domain.Common.EntityBase;

namespace PhotoGallery.Domain.Like;

public class Like : EntityBase
{
    public Guid UserId { get; protected set; }
    public Guid PhotoId { get; protected set; }
    public bool IsPositive { get; protected set; }

    public static Like Create(Guid userId, Guid photoId, bool isPositive)
    {
        var like = new Like
        {
            UserId = userId,
            PhotoId = photoId,
            IsPositive = isPositive
        };
        like.OnCreate();
        return like;
    }
}