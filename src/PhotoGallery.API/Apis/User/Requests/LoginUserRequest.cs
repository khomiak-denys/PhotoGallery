using PhotoGallery.UseCases.User.Login;

namespace PhotoGallery.API.Apis.Requests;

public class LoginUserRequest
{
    public string Login { get; set; }
    public string Password { get; set; }

    public LoginUserCommand ToCommand()
    {
        return new LoginUserCommand
        {
            Login = Login,
            Password = Password
        };
    }
}