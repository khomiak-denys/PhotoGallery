using AutoMapper;
using PhotoGallery.UseCases.Album.Common;

namespace PhotoGallery.UseCases.Mapping;

public class AlbumMappingProfile : Profile
{
    public AlbumMappingProfile()
    {
        CreateMap<Domain.Album.Album, AlbumResult>()
            .ForMember(dest => dest.CoverPath,
                source => source.MapFrom(src =>
                    src.Photos.FirstOrDefault() != null ? src.Photos.FirstOrDefault().Path : string.Empty));
    }
}