using PhotoGallery.Domain.Album;
using PhotoGallery.Domain.Photo;
using PhotoGallery.Domain.User;
using PhotoGallery.UseCases.Abstractions.Data;
using PhotoGallery.UseCases.Abstractions.Messaging;
using PhotoGallery.UseCases.Exceptions;

namespace PhotoGallery.UseCases.Album.Create;

public class CreateAlbumCommandHandler(
    IAlbumRepository albumRepository,
    IUserRepository userRepository,
    IUnitOfWork uow
    ) : ICommandHandler<CreateAlbumCommand, Guid>
{
    public async Task<Guid> Handle(CreateAlbumCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(command.UserId);
        if (user is null)
        {
            throw new NotFoundException("User", command.UserId);
        }

        var album = Domain.Album.Album.Create(command.Name, user, Enumerable.Empty<Domain.Photo.Photo>());

        albumRepository.Add(album);
        await uow.SaveChangesAsync(cancellationToken);

        return album.Id;
    }
}
