using Microsoft.AspNetCore.Mvc;
using PhotoGallery.API.Apis.Album.Requests;
using PhotoGallery.API.Apis.Responses;
using PhotoGallery.API.Common.Abstractions;
using PhotoGallery.Domain.Common.UserRoles;
using System.Security.Claims;

namespace PhotoGallery.API.Apis;

public class UploadPhotoEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/albums/{id:guid}/photos/upload", UploadPhoto)
            .RequireAuthorization()
            .Produces<UploadPhotoResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> UploadPhoto(
        Guid id,
        UploadPhotoRequest request,
        [AsParameters] PhotoServices services)
    {
        var user = services.HttpContextAccessor.HttpContext?.User;

        var userIdClaim = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var roleClaim = user?.FindFirst(ClaimTypes.Role)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(roleClaim))
        {
            return Results.Unauthorized();
        }

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return Results.Unauthorized();
        }

        if (!Enum.TryParse<UserRoles>(roleClaim, out var userRole))
        {
            return Results.Unauthorized();
        }

        var result = await services.Mediator.Send(request.ToCommand(id, userId, userRole));

        var response = new UploadPhotoResponse
        {
            PhotoId = result.PhotoId,
            ObjectKey = result.ObjectKey,
            UploadUrl = result.UploadUrl
        };

        return Results.Ok(response);
    }
}
