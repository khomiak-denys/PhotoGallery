using PhotoGallery.Domain.Album;
using PhotoGallery.Domain.Common.UserRoles;
using PhotoGallery.UseCases.Abstractions.Data;
using PhotoGallery.UseCases.Abstractions.Messaging;
using PhotoGallery.UseCases.Exceptions;

namespace PhotoGallery.UseCases.Album.Delete;

public class DeleteAlbumCommandHandler(
    IAlbumRepository albumRepository,
    IUnitOfWork uow
    ) : ICommandHandler<DeleteAlbumCommand>
{
    public async Task Handle(DeleteAlbumCommand command, CancellationToken cancellationToken)
    {
        var album = await albumRepository.GetById(command.AlbumId);
        if (album is null)
        {
            throw new NotFoundException("Album", command.AlbumId);
        }

        if (command.UserRole != UserRoles.Admin && album.OwnerId != command.UserId)
        {
            throw new ForbiddenException("User is not the album owner.");
        }

        albumRepository.Remove(album);
        await uow.SaveChangesAsync(cancellationToken);
    }
}
