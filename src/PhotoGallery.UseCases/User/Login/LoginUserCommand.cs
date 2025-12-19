using PhotoGallery.UseCases.Abstractions.Messaging;
using PhotoGallery.UseCases.User.Common;

namespace PhotoGallery.UseCases.User.Login;

public class LoginUserCommand : ICommand<LoginResult>
{
    public string Login { get; init; }
    public string Password { get; init; }
}