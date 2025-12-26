using Microsoft.AspNetCore.Mvc;
using PhotoGallery.API.Apis.Album.Requests;
using PhotoGallery.API.Apis.Responses;
using PhotoGallery.API.Common.Abstractions;

namespace PhotoGallery.API.Apis;

public class GetAlbumPhotosEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/albums/{id:guid}/photos", GetAlbumPhotos)
            .AllowAnonymous();
    }

    private static async Task<IResult> GetAlbumPhotos(
        Guid id,
        [AsParameters] GetAlbumPhotosRequest request,
        [AsParameters] PhotoServices services)
    {
        var result = await services.Mediator.Send(request.ToQuery(id));

        var response = await Task.WhenAll(result.Select(async photo => new PhotoWithLikesResponse
        {
            Id = photo.Id,
            Url = await services.ObjectStorageService.GetFileUrl(photo.Path),
            Likes = photo.Likes,
            Dislikes = photo.Dislikes
        }));

        return Results.Ok(response);
    }
}
