using PhotoGallery.API.Apis.Responses;
using PhotoGallery.API.Common.Abstractions;
using PhotoGallery.UseCases.Album.GetById;

namespace PhotoGallery.API.Apis;

public class GetAlbumByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/albums/{id:guid}", GetAlbumById)
            .AllowAnonymous();
    }

    private static async Task<IResult> GetAlbumById(Guid id, [AsParameters] AlbumServices services)
    {
        var query = new GetAlbumByIdQuery(id);
        var result = await services.Mediator.Send(query);

        var response = new AlbumResponse
        {
            Id = result.Id,
            Name = result.Name,
            CoverUrl = await services.ObjectStorageService.GetFileUrl(result.CoverPath)
        };
        return Results.Ok(response);
    }
}
