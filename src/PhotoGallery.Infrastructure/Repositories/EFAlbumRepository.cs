using PhotoGallery.Domain.Album;
using PhotoGallery.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace PhotoGallery.Infrastructure.Repositories;

public class EFAlbumRepository(
    AppDbContext context
    ) : IAlbumRepository
{
    public void Add(Album album)
    {
        context.Albums.Add(album);
    }

    public Task<List<Album>> GetAll(int page = 1, int pageSize = 5)
    {
        return context.Albums.Include(a => a.Photos.Take(1))
            .OrderBy(a => a.Id)
            .Skip(pageSize * (page - 1))
            .Take(pageSize)
            .ToListAsync();
    }
}