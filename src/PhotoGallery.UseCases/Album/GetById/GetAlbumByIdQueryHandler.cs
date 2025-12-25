using AutoMapper;
using PhotoGallery.Domain.Album;
using PhotoGallery.UseCases.Abstractions.Messaging;
using PhotoGallery.UseCases.Album.Common;
using PhotoGallery.UseCases.Exceptions;

namespace PhotoGallery.UseCases.Album.GetById;

public class GetAlbumByIdQueryHandler(
    IAlbumRepository albumRepository,
    IMapper mapper
    ) : IQueryHandler<GetAlbumByIdQuery, AlbumResult>
{
    public async Task<AlbumResult> Handle(GetAlbumByIdQuery query, CancellationToken cancellationToken)
    {
        var album = await albumRepository.GetById(query.Id);
        if (album is null)
        {
            throw new NotFoundException("Album", query.Id);
        }

        var result = mapper.Map<AlbumResult>(album);
        return result;
    }
}
