using Microsoft.EntityFrameworkCore;
using PhotoGallery.Domain.Album;
using PhotoGallery.Domain.Like;
using PhotoGallery.Domain.Photo;
using PhotoGallery.Domain.User;

namespace PhotoGallery.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}
    
    public DbSet<User> Users { get; set; }
    public DbSet<Photo>  Photos { get; set; }
    public DbSet<Album>  Albums { get; set; }
    public DbSet<Like> Likes { get; set; }
}