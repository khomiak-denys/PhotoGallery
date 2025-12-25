using PhotoGallery.Domain.Like;
using PhotoGallery.Domain.Photo;
using PhotoGallery.UseCases.Abstractions.Data;
using PhotoGallery.UseCases.Abstractions.Messaging;
using PhotoGallery.UseCases.Exceptions;

namespace PhotoGallery.UseCases.Photo.Like;

public class LikePhotoCommandHandler(
    IPhotoRepository photoRepository,
    ILikeRepository likeRepository,
    IUnitOfWork uow
    ) : ICommandHandler<LikePhotoCommand>
{
    public async Task Handle(LikePhotoCommand command, CancellationToken cancellationToken)
    {
        var photo = await photoRepository.GetById(command.PhotoId);
        if (photo is null)
        {
            throw new NotFoundException("Photo", command.PhotoId);
        }

        var like = Domain.Like.Like.Create(command.UserId, command.PhotoId, command.IsPositive);
        likeRepository.Add(like);
        await uow.SaveChangesAsync(cancellationToken);
    }
}
