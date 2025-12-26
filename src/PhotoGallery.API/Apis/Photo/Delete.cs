using Microsoft.AspNetCore.Mvc;
using PhotoGallery.API.Common.Abstractions;
using PhotoGallery.Domain.Common.UserRoles;
using PhotoGallery.UseCases.Photo.Delete;
using System.Security.Claims;

namespace PhotoGallery.API.Apis;

public class DeletePhotoEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/photos/{id:guid}", DeletePhoto)
            .RequireAuthorization()
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status403Forbidden);
    }

    private static async Task<IResult> DeletePhoto(Guid id, [AsParameters] PhotoServices services)
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

        await services.Mediator.Send(new DeletePhotoCommand
        {
            PhotoId = id,
            UserId = userId,
            UserRole = userRole
        });

        return Results.NoContent();
    }
}
