using System.ComponentModel;

namespace PhotoGallery.Domain.User;

public interface IUserRepository
{
    public Task<User?> GetByLoginAsync(string login);
    public Task<User?> GetByIdAsync(Guid id);
    public void Add(User user);
}