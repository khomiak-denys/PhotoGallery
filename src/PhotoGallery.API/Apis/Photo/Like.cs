using Microsoft.AspNetCore.Mvc;
using PhotoGallery.API.Apis.Photo.Requests;
using PhotoGallery.API.Common.Abstractions;

namespace PhotoGallery.API.Apis;

public class LikePhotoEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/photos/{id:guid}/like", LikePhoto)
            .RequireAuthorization()
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> LikePhoto(
        Guid id,
        LikePhotoRequest request,
        [AsParameters] PhotoServices services)
    {
        var user = services.HttpContextAccessor.HttpContext?.User;

        var userIdClaim = user?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Results.Unauthorized();
        }

        await services.Mediator.Send(request.ToCommand(id, userId));
        return Results.NoContent();
    }
}
