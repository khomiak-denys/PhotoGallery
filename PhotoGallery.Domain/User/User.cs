using PhotoGallery.Domain.Common.EntityBase;
using PhotoGallery.Domain.Common.UserRoles;

namespace PhotoGallery.Domain.User;

public class User : EntityBase
{
    public string Login { get; protected set; }
    public string Password { get; protected set; }
    public string FirstName { get; protected set; }
    public string LastName { get; protected set; }
    public UserRoles Role { get; protected set; }

    public static User Create(string login, string password, string firstName, string lastName, UserRoles role)
    {
        var user = new User
        {
            Login = login,
            Password = password,
            FirstName = firstName,
            LastName = lastName,
            Role = role
        };
        user.OnCreate();
        return user;
    }
}



