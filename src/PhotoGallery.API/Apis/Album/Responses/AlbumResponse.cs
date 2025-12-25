namespace PhotoGallery.API.Apis.Responses;

public class AlbumResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string CoverUrl { get; init; }
}