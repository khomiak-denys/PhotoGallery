using AutoMapper;
using PhotoGallery.API.Apis.Reponses;
using PhotoGallery.UseCases.User.Common;

namespace PhotoGallery.API.Mapping;

public class LoginMappingProfile : Profile
{
    public LoginMappingProfile() {
        CreateMap<LoginResult, LoginResponse>();
    }
}