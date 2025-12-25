using AutoMapper;
using PhotoGallery.Domain.Album;
using PhotoGallery.UseCases.Abstractions.Messaging;
using PhotoGallery.UseCases.Album.Common;

namespace PhotoGallery.UseCases.Album.GetAll;

public class GetAlbumsQueryHandler(
    IAlbumRepository albumRepository,
    IMapper mapper
    ) : IQueryHandler<GetAlbumsQuery, List<AlbumResult>>
{
    public async Task<List<AlbumResult>> Handle(GetAlbumsQuery query, CancellationToken cancellationToken)
    {
        var albums = albumRepository.GetAll(query.Page, query.PageSize);
        
        var result = mapper.Map<List<AlbumResult>>(albums);
        
        return result;
    }
}