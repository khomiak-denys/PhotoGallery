using PhotoGallery.UseCases.User.Register;

namespace PhotoGallery.API.Apis.Requests;

public class RegisterUserRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }

    public RegisterUserCommand ToCommand()
    {
        return new RegisterUserCommand
        {
            Login = Login,
            Password = Password,
            FirstName = FirstName,
            LastName = LastName
        };
    }

}