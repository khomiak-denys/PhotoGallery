using PhotoGallery.Domain.Common.EntityBase;

namespace PhotoGallery.Domain.Photo;

public class Photo : EntityBase
{
    public string Path { get; protected set; }
    public Guid? AlbumId { get; protected set; }

    public static Photo Create(string path, Guid? albumId = null)
    {
        var photo = new Photo
        {
            Path = path,
            AlbumId = albumId
        };
        photo.OnCreate();
        return photo;
    }
}
