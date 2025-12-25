using Microsoft.AspNetCore.Mvc;
using PhotoGallery.API.Apis.Responses;
using PhotoGallery.API.Common.Abstractions;
using PhotoGallery.UseCases.Photo.GetAll;

namespace PhotoGallery.API.Apis;

public class GetAllPhotosEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/photos", GetPhotos)
            .AllowAnonymous();
    }

    private static async Task<IResult> GetPhotos([AsParameters] GetPhotosQuery query, [AsParameters] PhotoServices services)
    {
        var result = await services.Mediator.Send(query);

        var response = await Task.WhenAll(result.Select(async photo => new PhotoResponse
        {
            Id = photo.Id,
            Url = await services.ObjectStorageService.GetFileUrl(photo.Path)
        }));

        return Results.Ok(response);
    }
}
