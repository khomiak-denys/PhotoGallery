using PhotoGallery.Domain.Like;
using PhotoGallery.Infrastructure.Database;

namespace PhotoGallery.Infrastructure.Repositories;

public class EFLikeRepository(
    AppDbContext context
    ) : ILikeRepository
{
    public void Add(Like like)
    {
        context.Likes.Add(like);
    }
}
