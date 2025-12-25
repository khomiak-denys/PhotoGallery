using Microsoft.AspNetCore.Mvc;
using PhotoGallery.API.Apis.Album.Requests;
using PhotoGallery.API.Common.Abstractions;

namespace PhotoGallery.API.Apis;

public class CreateAlbumEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/albums", CreateAlbum)
            .RequireAuthorization()
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> CreateAlbum(
        CreateAlbumRequest request,
        [AsParameters] AlbumServices services)
    {
        var user = services.HttpContextAccessor.HttpContext?.User;

        var userIdClaim = user?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Results.Unauthorized();
        }

        var albumId = await services.Mediator.Send(request.ToCommand(userId));
        return Results.Created($"/api/v1/albums/{albumId}", new { id = albumId });
    }
}
