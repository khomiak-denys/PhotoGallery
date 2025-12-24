namespace PhotoGallery.API.Apis.Auth.Responses;

public record LoginResponse
(
    Guid Id,
    string FirstName,
    string LastName,
    string Login,
    string Token 
);