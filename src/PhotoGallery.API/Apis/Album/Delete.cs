using Microsoft.AspNetCore.Mvc;
using PhotoGallery.API.Common.Abstractions;
using PhotoGallery.UseCases.Album.Delete;

namespace PhotoGallery.API.Apis;

public class DeleteAlbumEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/albums/{id:guid}", DeleteAlbum)
            .RequireAuthorization()
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status403Forbidden);
    }

    private static async Task<IResult> DeleteAlbum(Guid id, [AsParameters] AlbumServices services)
    {
        var user = services.HttpContextAccessor.HttpContext?.User;

        var userIdClaim = user?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Results.Unauthorized();
        }

        await services.Mediator.Send(new DeleteAlbumCommand
        {
            AlbumId = id,
            UserId = userId
        });

        return Results.NoContent();
    }
}
