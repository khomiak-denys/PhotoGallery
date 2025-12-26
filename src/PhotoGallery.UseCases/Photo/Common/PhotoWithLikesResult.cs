namespace PhotoGallery.UseCases.Photo.Common;

public class PhotoWithLikesResult
{
    public Guid Id { get; init; }
    public string Path { get; init; } = string.Empty;
    public int Likes { get; init; }
    public int Dislikes { get; init; }
}
