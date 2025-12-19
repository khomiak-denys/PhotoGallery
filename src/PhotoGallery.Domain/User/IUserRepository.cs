using System.ComponentModel;

namespace PhotoGallery.Domain.User;

public interface IUserRepository
{
    public Task<User?> GetByLoginAsync(string login);
    public void Add(User user);
}