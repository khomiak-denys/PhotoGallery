using PhotoGallery.UseCases.Abstractions.Messaging;
using PhotoGallery.UseCases.Album.Common;

namespace PhotoGallery.UseCases.Album.GetMy;

public class GetMyAlbumsQuery : IQuery<List<AlbumResult>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }
    public Guid UserId { get; init; }
}