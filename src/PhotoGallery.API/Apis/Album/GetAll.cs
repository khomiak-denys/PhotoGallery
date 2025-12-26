using Microsoft.AspNetCore.Mvc;
using PhotoGallery.API.Apis.Responses;
using PhotoGallery.API.Common.Abstractions;
using PhotoGallery.UseCases.Album.GetAll;

namespace PhotoGallery.API.Apis;

public class GetAllAlbumsEndpoint : IEndpoint 
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/albums", GetAlbums)
            .AllowAnonymous();
    }

    private static async Task<IResult> GetAlbums([AsParameters] GetAlbumsQuery query, [AsParameters] AlbumServices services)
    {  
        var result = await services.Mediator.Send(query);

        var response = await Task.WhenAll(result.Select(async album => new AlbumResponse
        {
            Id = album.Id,
            Name = album.Name,
            CoverUrl = await services.ObjectStorageService.GetFileUrl(album.CoverPath)
        }));
        return Results.Ok(response);
    }
}
