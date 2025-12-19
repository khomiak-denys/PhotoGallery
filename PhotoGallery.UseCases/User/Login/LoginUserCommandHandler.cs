using PhotoGallery.Domain.User;
using PhotoGallery.UseCases.Abstractions.Messaging;
using PhotoGallery.UseCases.Abstractions.Services;
using PhotoGallery.UseCases.Exceptions;
using PhotoGallery.UseCases.User.Common;

namespace PhotoGallery.UseCases.User.Login;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, LoginResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public LoginUserCommandHandler(
        IUserRepository userRepository, 
        IPasswordHasher passwordHasher,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<LoginResult> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByLoginAsync(command.Login);
        if (existingUser == null)
        {
            throw new NotFoundException("User not found");
        }

        if (!_passwordHasher.VerifyPassword(command.Password, existingUser.PasswordHash))
        {
            throw new InvalidArgumentException("Invalid credentials");
        }

        var token = await _tokenService.GetToken(existingUser.Id, existingUser.Login, existingUser.Role);

        return new LoginResult
        (
            existingUser.Id,
            existingUser.FirstName,
            existingUser.LastName,
            existingUser.Login,
            token
        );
    }
}