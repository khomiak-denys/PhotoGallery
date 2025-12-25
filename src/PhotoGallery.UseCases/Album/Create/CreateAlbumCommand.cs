using PhotoGallery.UseCases.Abstractions.Messaging;

namespace PhotoGallery.UseCases.Album.Create;

public class CreateAlbumCommand : ICommand<Guid>
{
    public string Name { get; init; } = string.Empty;
    public Guid UserId { get; init; }
}
