namespace PhotoGallery.UseCases.User.Common;

public record LoginResult
(
    Guid Id,
    string FirstName,
    string LastName,
    string Login,
    string Token
);