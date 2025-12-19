using PhotoGallery.Domain.Common.EntityBase;
using PhotoGallery.Domain.Common.UserRoles;

namespace PhotoGallery.Domain.User;

public class User : EntityBase
{
    public string Login { get; protected set; }
    public string PasswordHash { get; protected set; }
    public string FirstName { get; protected set; }
    public string LastName { get; protected set; }
    public UserRoles Role { get; protected set; }

    public static User Create(string login, string passwordHash, string firstName, string lastName)
    {
        var user = new User
        {
            Login = login,
            PasswordHash = passwordHash,
            FirstName = firstName,
            LastName = lastName,
            Role = UserRoles.User
        };
        user.OnCreate();
        return user;
    }
}



