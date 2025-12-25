using Microsoft.EntityFrameworkCore;
using PhotoGallery.Domain.Photo;
using PhotoGallery.Infrastructure.Database;

namespace PhotoGallery.Infrastructure.Repositories;

public class EFPhotoRepository(
    AppDbContext context
    ) : IPhotoRepository
{
    public void Add(Photo photo)
    {
        context.Photos.Add(photo);
    }

    public void Remove(Photo photo)
    {
        context.Photos.Remove(photo);
    }

    public Task<List<Photo>> GetAll(int page = 1, int pageSize = 5)
    {
        return context.Photos
            .OrderBy(p => p.Id)
            .Skip(pageSize * (page - 1))
            .Take(pageSize)
            .ToListAsync();
    }

    public Task<Photo?> GetById(Guid id)
    {
        return context.Photos.FirstOrDefaultAsync(p => p.Id == id);
    }
}
