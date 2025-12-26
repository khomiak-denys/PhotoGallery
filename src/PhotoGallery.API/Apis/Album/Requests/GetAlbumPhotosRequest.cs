using PhotoGallery.UseCases.Photo.GetByAlbum;

namespace PhotoGallery.API.Apis.Album.Requests;

public class GetAlbumPhotosRequest
{
    public int Page { get; init; }
    public int PageSize { get; init; }

    public GetAlbumPhotosQuery ToQuery(Guid albumId)
    {
        return new GetAlbumPhotosQuery
        {
            AlbumId = albumId,
            Page = Page,
            PageSize = PageSize
        };
    }
}
