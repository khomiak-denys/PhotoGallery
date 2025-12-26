using Microsoft.EntityFrameworkCore;
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

    public async Task<Dictionary<Guid, LikeSummary>> GetSummaryByPhotoIds(IEnumerable<Guid> photoIds)
    {
        var ids = photoIds.Distinct().ToList();
        if (ids.Count == 0)
        {
            return new Dictionary<Guid, LikeSummary>();
        }

        var summaries = await context.Likes
            .Where(like => ids.Contains(like.PhotoId))
            .GroupBy(like => like.PhotoId)
            .Select(group => new
            {
                PhotoId = group.Key,
                Likes = group.Count(like => like.IsPositive),
                Dislikes = group.Count(like => !like.IsPositive)
            })
            .ToListAsync();

        return summaries.ToDictionary(
            summary => summary.PhotoId,
            summary => new LikeSummary(summary.Likes, summary.Dislikes));
    }
}
