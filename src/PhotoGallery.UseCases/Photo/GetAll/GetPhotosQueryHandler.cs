using AutoMapper;
using PhotoGallery.Domain.Photo;
using PhotoGallery.UseCases.Abstractions.Messaging;
using PhotoGallery.UseCases.Photo.Common;

namespace PhotoGallery.UseCases.Photo.GetAll;

public class GetPhotosQueryHandler(
    IPhotoRepository photoRepository,
    IMapper mapper
    ) : IQueryHandler<GetPhotosQuery, List<PhotoResult>>
{
    public async Task<List<PhotoResult>> Handle(GetPhotosQuery query, CancellationToken cancellationToken)
    {
        var photos = await photoRepository.GetAll(query.Page, query.PageSize);

        var result = mapper.Map<List<PhotoResult>>(photos);
        return result;
    }
}
