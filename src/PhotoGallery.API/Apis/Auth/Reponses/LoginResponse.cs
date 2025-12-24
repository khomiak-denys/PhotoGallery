namespace PhotoGallery.API.Apis.Reponses;

public record LoginResponse
(
    Guid Id,
    string FirstName,
    string LastName,
    string Login,
    string Token 
);