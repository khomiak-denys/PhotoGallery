using PhotoGallery.UseCases.Abstractions.Messaging;
using PhotoGallery.UseCases.Album.Common;

namespace PhotoGallery.UseCases.Album.GetAll;

public class GetAlbumsQuery : IQuery<List<AlbumResult>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }
}