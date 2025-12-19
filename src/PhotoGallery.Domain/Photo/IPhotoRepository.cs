namespace PhotoGallery.Domain.Photo;

public interface IPhotoRepository
{
    public void Add(Photo photo);
    public void Remove(Photo photo);
}