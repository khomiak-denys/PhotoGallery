using Microsoft.AspNetCore.Mvc;
using PhotoGallery.API.Apis.Responses;
using PhotoGallery.API.Common.Abstractions;
using PhotoGallery.UseCases.Album.GetAll;

namespace PhotoGallery.API.Apis;

public class GetAll : IEndpoint 
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/albums", GetAlbums)
            .AllowAnonymous();
    }

    private static async Task<IResult> GetAlbums([AsParameters] GetAlbumsQuery query, [AsParameters] AlbumServices services)
    {  
        var result = await services.Mediator.Send(query);
        
        var response = services.Mapper.Map<List<AlbumResponse>>(result);
        return Results.Ok(response);
    }
}