using Microsoft.EntityFrameworkCore;
using PhotoGallery.Domain.User;
using PhotoGallery.Infrastructure.Database;

namespace PhotoGallery.Infrastructure.Repositories;

public class EFUserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;
    
    public EFUserRepository(AppDbContext appDbContext)
    {
        _dbContext = appDbContext;
    }
    
    public async Task<User?> GetByLoginAsync(string login)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Login == login);
    }
    
    public void Add(User user)
    {
        _dbContext.Users.Add(user);
    }
}