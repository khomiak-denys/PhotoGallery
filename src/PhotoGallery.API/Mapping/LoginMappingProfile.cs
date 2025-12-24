using AutoMapper;
using PhotoGallery.API.Apis.Auth.Responses;
using PhotoGallery.UseCases.User.Common;

namespace PhotoGallery.API.Mapping;

public class LoginMappingProfile : Profile
{
    public LoginMappingProfile() {
        CreateMap<LoginResult, LoginResponse>();
    }
}