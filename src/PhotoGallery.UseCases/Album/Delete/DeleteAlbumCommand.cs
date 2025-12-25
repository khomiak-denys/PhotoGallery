using PhotoGallery.UseCases.Abstractions.Messaging;

namespace PhotoGallery.UseCases.Album.Delete;

public class DeleteAlbumCommand : ICommand
{
    public Guid AlbumId { get; init; }
    public Guid UserId { get; init; }
}
