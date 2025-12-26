using PhotoGallery.UseCases.Abstractions.Messaging;
using PhotoGallery.UseCases.Photo.Common;

namespace PhotoGallery.UseCases.Photo.GetByAlbum;

public class GetAlbumPhotosQuery : IQuery<List<PhotoWithLikesResult>>
{
    public Guid AlbumId { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}
