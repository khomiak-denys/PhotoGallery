namespace PhotoGallery.Domain.Album;

public interface IAlbumRepository
{
    public void Add(Album album);
    public Task<List<Album>> GetAll(int page = 1, int pageSize = 5);
    public Task<List<Album>> GetByUserId(Guid queryUserId, int page = 1, int pageSize = 5);
}