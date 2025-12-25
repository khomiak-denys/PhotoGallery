using PhotoGallery.UseCases.Album.Create;

namespace PhotoGallery.API.Apis.Album.Requests;

public class CreateAlbumRequest
{
    public string Name { get; init; } = string.Empty;

    public CreateAlbumCommand ToCommand(Guid userId)
    {
        return new CreateAlbumCommand
        {
            Name = Name,
            UserId = userId
        };
    }
}
