using AutoMapper;
using MediatR;

namespace PhotoGallery.API.Apis.Auth;

public class AuthServices(
    IMediator mediator,
    IMapper mapper)
{
    public IMediator Mediator => mediator;
    public IMapper Mapper => mapper;
}