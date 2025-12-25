using PhotoGallery.API.Apis.Responses;
using PhotoGallery.API.Common.Abstractions;
using PhotoGallery.UseCases.Photo.GetById;

namespace PhotoGallery.API.Apis;

public class GetPhotoByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/photos/{id:guid}", GetPhotoById)
            .AllowAnonymous();
    }

    private static async Task<IResult> GetPhotoById(Guid id, [AsParameters] PhotoServices services)
    {
        var result = await services.Mediator.Send(new GetPhotoByIdQuery(id));

        var response = new PhotoResponse
        {
            Id = result.Id,
            Url = await services.ObjectStorageService.GetFileUrl(result.Path)
        };

        return Results.Ok(response);
    }
}
