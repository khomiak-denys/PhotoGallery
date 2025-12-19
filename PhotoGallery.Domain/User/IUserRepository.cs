using System.ComponentModel;

namespace PhotoGallery.Domain.User;

public interface IUserRepository
{
    public User GetByLogin(string login);
    public void Add(User user);
}