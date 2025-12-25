using AutoMapper;
using MediatR;

namespace PhotoGallery.API.Apis;

public class AlbumServices(
    IMediator mediator,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
{
    public IMediator Mediator => mediator;
    public IMapper Mapper => mapper;
    public IHttpContextAccessor HttpContextAccessor => httpContextAccessor;
}