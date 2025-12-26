namespace PhotoGallery.Domain.Photo;

public interface IPhotoRepository
{
    public void Add(Photo photo);
    public void Remove(Photo photo);
    public Task<List<Photo>> GetAll(int page = 1, int pageSize = 5);
    public Task<List<Photo>> GetByAlbumId(Guid albumId, int page = 1, int pageSize = 5);
    public Task<Photo?> GetById(Guid id);
}
