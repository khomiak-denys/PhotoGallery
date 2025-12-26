namespace PhotoGallery.API.Apis.Responses;

public class PhotoWithLikesResponse
{
    public Guid Id { get; init; }
    public string Url { get; init; } = string.Empty;
    public int Likes { get; init; }
    public int Dislikes { get; init; }
}
