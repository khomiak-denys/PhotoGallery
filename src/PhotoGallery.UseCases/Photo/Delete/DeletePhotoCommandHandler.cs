using PhotoGallery.Domain.Album;
using PhotoGallery.Domain.Common.UserRoles;
using PhotoGallery.Domain.Photo;
using PhotoGallery.UseCases.Abstractions.Data;
using PhotoGallery.UseCases.Abstractions.Messaging;
using PhotoGallery.UseCases.Exceptions;

namespace PhotoGallery.UseCases.Photo.Delete;

public class DeletePhotoCommandHandler(
    IPhotoRepository photoRepository,
    IAlbumRepository albumRepository,
    IUnitOfWork uow
    ) : ICommandHandler<DeletePhotoCommand>
{
    public async Task Handle(DeletePhotoCommand command, CancellationToken cancellationToken)
    {
        var photo = await photoRepository.GetById(command.PhotoId);
        if (photo is null)
        {
            throw new NotFoundException("Photo", command.PhotoId);
        }

        if (command.UserRole != UserRoles.Admin)
        {
            if (photo.AlbumId is null)
            {
                throw new ForbiddenException("User is not the album owner.");
            }

            var album = await albumRepository.GetById(photo.AlbumId.Value);
            if (album is null)
            {
                throw new NotFoundException("Album", photo.AlbumId.Value);
            }

            if (album.OwnerId != command.UserId)
            {
                throw new ForbiddenException("User is not the album owner.");
            }
        }

        photoRepository.Remove(photo);
        await uow.SaveChangesAsync(cancellationToken);
    }
}
