using PhotoGallery.UseCases.Abstractions.Messaging;
using PhotoGallery.UseCases.Photo.Common;

namespace PhotoGallery.UseCases.Photo.GetAll;

public class GetPhotosQuery : IQuery<List<PhotoResult>>
{
    public int Page { get; init; }
    public int PageSize { get; init; }
}
