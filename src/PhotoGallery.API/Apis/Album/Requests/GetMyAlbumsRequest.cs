using PhotoGallery.UseCases.Album.GetMy;

namespace PhotoGallery.API.Apis.Album.Requests;

public class GetMyAlbumsRequest
{
    public int Page { get; init; }
    public int PageSize { get; init; }

    public GetMyAlbumsQuery ToQuery(Guid userId)
    {
        return new GetMyAlbumsQuery
        {
            Page = Page,
            PageSize = PageSize,
            UserId = userId
        };
    }
}