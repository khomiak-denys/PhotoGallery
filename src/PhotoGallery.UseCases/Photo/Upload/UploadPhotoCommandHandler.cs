using System.IO;
using PhotoGallery.Domain.Album;
using PhotoGallery.Domain.Common.UserRoles;
using PhotoGallery.Domain.Photo;
using PhotoGallery.UseCases.Abstractions.Data;
using PhotoGallery.UseCases.Abstractions.Messaging;
using PhotoGallery.UseCases.Abstractions.Services;
using PhotoGallery.UseCases.Exceptions;

namespace PhotoGallery.UseCases.Photo.Upload;

public class UploadPhotoCommandHandler(
    IAlbumRepository albumRepository,
    IPhotoRepository photoRepository,
    IObjectStorageService objectStorageService,
    IUnitOfWork uow
    ) : ICommandHandler<UploadPhotoCommand, UploadPhotoResult>
{
    public async Task<UploadPhotoResult> Handle(UploadPhotoCommand command, CancellationToken cancellationToken)
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

        var extension = Path.GetExtension(command.FileName);
        if (string.IsNullOrWhiteSpace(extension))
        {
            throw new InvalidArgumentException("File extension is required.");
        }

        var objectKey = $"albums/{command.AlbumId}/{Guid.NewGuid()}{extension}";
        var uploadUrl = await objectStorageService.GetUploadUrl(objectKey);

        var photo = Domain.Photo.Photo.Create(objectKey, command.AlbumId);
        photoRepository.Add(photo);
        await uow.SaveChangesAsync(cancellationToken);

        return new UploadPhotoResult
        {
            PhotoId = photo.Id,
            ObjectKey = objectKey,
            UploadUrl = uploadUrl
        };
    }
}
