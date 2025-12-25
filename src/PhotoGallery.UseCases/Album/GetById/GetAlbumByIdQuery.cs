using PhotoGallery.UseCases.Abstractions.Messaging;
using PhotoGallery.UseCases.Album.Common;

namespace PhotoGallery.UseCases.Album.GetById;

public class GetAlbumByIdQuery : IQuery<AlbumResult>
{
    public GetAlbumByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; init; }
}
