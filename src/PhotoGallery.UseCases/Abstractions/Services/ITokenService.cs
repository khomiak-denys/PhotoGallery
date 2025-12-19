using PhotoGallery.Domain.Common.UserRoles;

namespace PhotoGallery.UseCases.Abstractions.Services;

public interface ITokenService
{
    public Task<string> GetToken(Guid id, string login, UserRoles role);
}