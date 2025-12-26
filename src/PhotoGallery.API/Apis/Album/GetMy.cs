using Microsoft.AspNetCore.Mvc;
using PhotoGallery.API.Apis.Album.Requests;
using PhotoGallery.API.Apis.Requests;
using PhotoGallery.API.Apis.Responses;
using PhotoGallery.API.Common.Abstractions;
using PhotoGallery.Domain.User;
using PhotoGallery.UseCases.Album.GetAll;

namespace PhotoGallery.API.Apis;

public class GetMyAlbumsEndpoint : IEndpoint 
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/albums/my", GetMyAlbums)
            .RequireAuthorization();
    }

    private static async Task<IResult> GetMyAlbums([AsParameters] GetMyAlbumsRequest request, [AsParameters] AlbumServices services)
    {  
        var user = services.HttpContextAccessor.HttpContext?.User;

        var userIdClaim = user?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
    
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Results.Unauthorized();
        }
        
        var result = await services.Mediator.Send(request.ToQuery(userId));

        var response = await Task.WhenAll(result.Select(async album => new AlbumResponse
        {
            Id = album.Id,
            Name = album.Name,
            CoverUrl = await services.ObjectStorageService.GetFileUrl(album.CoverPath)
        }));
        return Results.Ok(response);
    }
}
