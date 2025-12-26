namespace PhotoGallery.Domain.Like;

public interface ILikeRepository
{
    public void Add(Like like);
    public Task<Dictionary<Guid, LikeSummary>> GetSummaryByPhotoIds(IEnumerable<Guid> photoIds);
}
