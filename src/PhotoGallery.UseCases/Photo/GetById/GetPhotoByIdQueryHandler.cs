using AutoMapper;
using PhotoGallery.Domain.Photo;
using PhotoGallery.UseCases.Abstractions.Messaging;
using PhotoGallery.UseCases.Exceptions;
using PhotoGallery.UseCases.Photo.Common;

namespace PhotoGallery.UseCases.Photo.GetById;

public class GetPhotoByIdQueryHandler(
    IPhotoRepository photoRepository,
    IMapper mapper
    ) : IQueryHandler<GetPhotoByIdQuery, PhotoResult>
{
    public async Task<PhotoResult> Handle(GetPhotoByIdQuery query, CancellationToken cancellationToken)
    {
        var photo = await photoRepository.GetById(query.Id);
        if (photo is null)
        {
            throw new NotFoundException("Photo", query.Id);
        }

        var result = mapper.Map<PhotoResult>(photo);
        return result;
    }
}
