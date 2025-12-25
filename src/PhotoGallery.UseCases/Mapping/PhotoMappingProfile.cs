using AutoMapper;
using PhotoGallery.UseCases.Photo.Common;

namespace PhotoGallery.UseCases.Mapping;

public class PhotoMappingProfile : Profile
{
    public PhotoMappingProfile()
    {
        CreateMap<Domain.Photo.Photo, PhotoResult>();
    }
}
