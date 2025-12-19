using PhotoGallery.Domain.User;
using PhotoGallery.UseCases.Abstractions.Data;
using PhotoGallery.UseCases.Abstractions.Messaging;
using PhotoGallery.UseCases.Abstractions.Services;
using PhotoGallery.UseCases.Exceptions;

namespace PhotoGallery.UseCases.User.Register;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _uow;
    private readonly IPasswordHasher _passwordHasher;
    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork uow,
        IPasswordHasher passwordHasher
    )
    {
        _userRepository = userRepository;
        _uow = uow;
        _passwordHasher = passwordHasher;
    }
    public async Task<Guid> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByLoginAsync(command.Login);
        if (existingUser != null)
        {
            throw new AlreadyExistsException("User", command.Login);
        }
        var passwordHash = _passwordHasher.HashPassword(command.Password);
        var user = Domain.User.User.Create(command.Login, passwordHash, command.FirstName, command.LastName);

        _userRepository.Add(user);
        await _uow.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}