using AutoMapper;
using PhotoGallery.API.Apis.Responses;
using PhotoGallery.UseCases.Album.Common;

namespace PhotoGallery.API.Mapping;

public class AlbumMappingProfile : Profile
{
    public AlbumMappingProfile()
    {
        CreateMap<AlbumResult, AlbumResponse>()
            .ForMember(dest => dest.CoverUrl, opt => opt.MapFrom(src => src.CoverPath));
    }
}
