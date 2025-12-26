using AutoMapper;
using MediatR;
using PhotoGallery.UseCases.Abstractions.Services;

namespace PhotoGallery.API.Apis;

public class AlbumServices(
    IMediator mediator,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IObjectStorageService objectStorageService)
{
    public IMediator Mediator => mediator;
    public IMapper Mapper => mapper;
    public IHttpContextAccessor HttpContextAccessor => httpContextAccessor;
    public IObjectStorageService ObjectStorageService => objectStorageService;
}
