using AutoMapper;
using PhotoGallery.Domain.Album;
using PhotoGallery.Domain.User;
using PhotoGallery.UseCases.Abstractions.Messaging;
using PhotoGallery.UseCases.Album.Common;
using PhotoGallery.UseCases.Exceptions;

namespace PhotoGallery.UseCases.Album.GetMy;

public class GetMyAlbumsQueryHandler(
    IUserRepository userRepository,
    IAlbumRepository albumRepository,
    IMapper mapper) : IQueryHandler<GetMyAlbumsQuery, List<AlbumResult>>
{
    public async Task<List<AlbumResult>> Handle(GetMyAlbumsQuery query, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(query.UserId);

        if (user == null) throw new NotFoundException(nameof(User), query.UserId);

        var albums = await albumRepository.GetByUserId(query.UserId, query.Page, query.PageSize);

        var result = mapper.Map<List<AlbumResult>>(albums);

        return result;
    }
}