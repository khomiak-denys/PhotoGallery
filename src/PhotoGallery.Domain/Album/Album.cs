using PhotoGallery.Domain.Common.EntityBase;

namespace PhotoGallery.Domain.Album;

public class Album : EntityBase
{
    public string Name { get; protected set; }
    public User.User Owner { get; protected set; }
    public Guid OwnerId { get; protected set; }
    public IEnumerable<Photo.Photo>  Photos { get; protected set; }

    public static Album Create(string name, User.User user, IEnumerable<Photo.Photo> photos)
    {
        var album = new Album
        {
            Name = name,
            Owner = user,
            OwnerId = user.Id,
            Photos = photos
        };
        album.OnCreate();
        return album;
    }
}
