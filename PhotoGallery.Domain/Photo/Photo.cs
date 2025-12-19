using PhotoGallery.Domain.Common.EntityBase;

namespace PhotoGallery.Domain.Photo;

public class Photo : EntityBase
{
    public string Path { get; protected set; }

    public static Photo Create(string path)
    {
        var photo = new Photo
        {
            Path = path
        };
        photo.OnCreate();
        return photo;
    }
}