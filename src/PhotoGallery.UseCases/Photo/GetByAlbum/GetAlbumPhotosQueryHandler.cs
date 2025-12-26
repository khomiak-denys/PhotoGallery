using PhotoGallery.Domain.Like;
using PhotoGallery.Domain.Photo;
using PhotoGallery.UseCases.Abstractions.Messaging;
using PhotoGallery.UseCases.Photo.Common;

namespace PhotoGallery.UseCases.Photo.GetByAlbum;

public class GetAlbumPhotosQueryHandler(
    IPhotoRepository photoRepository,
    ILikeRepository likeRepository
    ) : IQueryHandler<GetAlbumPhotosQuery, List<PhotoWithLikesResult>>
{
    public async Task<List<PhotoWithLikesResult>> Handle(GetAlbumPhotosQuery query, CancellationToken cancellationToken)
    {
        var photos = await photoRepository.GetByAlbumId(query.AlbumId, query.Page, query.PageSize);
        var photoIds = photos.Select(photo => photo.Id).ToList();
        var summaries = await likeRepository.GetSummaryByPhotoIds(photoIds);

        var results = new List<PhotoWithLikesResult>(photos.Count);
        foreach (var photo in photos)
        {
            summaries.TryGetValue(photo.Id, out var summary);
            results.Add(new PhotoWithLikesResult
            {
                Id = photo.Id,
                Path = photo.Path,
                Likes = summary?.Likes ?? 0,
                Dislikes = summary?.Dislikes ?? 0
            });
        }

        return results;
    }
}
